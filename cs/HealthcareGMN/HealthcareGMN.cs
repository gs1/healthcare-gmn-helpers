using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GS1
{

    /// <summary>
    /// Helper class that is both a demonstration and usable implementation of a
    /// check character generator and verifier for a GS1 GMN that is used for
    /// Regulated Healthcare medical devices that fall under the EU regulations EU
    /// MDR 2017/745 and EU IVDR 2017/746, herein referred to as a "healthcare GMN".
    ///
    /// (c) 2019 GS1 AISBL. All rights reserved.
    /// </summary>
    public static class HealthcareGMN
    {

        /// <summary>
        /// Descending primes used as multipliers of each data character.
        /// </summary>
        private static readonly ushort[] weights = new ushort[] {83,79,73,71,67,61,59,53,47,43,41,37,31,29,23,19,17,13,11,7,5,3,2};

        /// <summary>
        /// GS1 AI encodable character set 82. Place in the string represents the character value.
        /// </summary>
        private const string cset82 = "!\"%&'()*+,-./0123456789:;<=>?ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Subset of the encodable character set used for check characters.
        /// </summary>
        private const string cset32 = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ";

        /// <summary>
        /// Character to value map for cset82.
        /// </summary>
        private static readonly IReadOnlyDictionary<char, ushort> cset82val;

        // Initialisation populates the cset82 mapping
        static HealthcareGMN()
        {
            IDictionary<char, ushort> tmp = new Dictionary<char, ushort>();
            for (ushort i = 0; i < cset82.Length; i++)
                tmp.Add(cset82[i], i);
            cset82val = new ReadOnlyDictionary<char, ushort>(tmp);
        }

        /// <summary>
        /// Character to value map for cset82.
        /// </summary>
        /// <param name="part">A partial healthcare GMN.</param>
        /// <returns>Two check characters.</returns>
        /// <exception cref="GS1Exception">If the format of the given healthcare GMN is invalid.</exception>
        public static string CheckCharacters(string part)
        {
            _FormatChecks(part, false);

            /*
             * The GMN check digit calculation is performed here.
             *
             */

            // Characters are compared with the rightmost weights
            int offset = 23 - part.Length;

            // Sum the products of the character values and corresponding weights modulo 1021
            int sum = 0;
            for (int i = 0; i < part.Length; i++)
            {
                    ushort c = cset82val[ part[i] ];
                    ushort w = weights[ offset + i ];
                    sum += c * w;
            }
            sum %= 1021;

            // Check characters are the upper and lower 5 bits of the 10-bit sum
            return "" + cset32[sum >> 5] + cset32[sum & 31];

            // Equivalently, C1 = INT(sum/32); C2 = sum MOD 32
            // return "" + cset32[sum / 32] + cset32[sum % 32];

        }

        /// <summary>
        /// Complete a given partial healthcare GMN by appending two check characters.
        /// </summary>
        /// <param name="part">A partial healthcare GMN.</param>
        /// <returns>A complete healthcare GMN including check characters.</returns>
        /// <exception cref="GS1Exception">If the format of the given healthcare GMN is invalid.</exception>
        public static string AddCheckCharacters(string part)
        {
            return part + CheckCharacters(part);
        }

        /// <summary>
        /// Verify that a given healthcare GMN has correct check characters.
        /// </summary>
        /// <param name="gmn">A healthcare GMN.</param>
        /// <returns>True if the healthcare GMN is has valid check characters. Otherwise false.</returns>
        /// <exception cref="GS1Exception">If the format of the given healthcare GMN is invalid.</exception>
        public static bool VerifyCheckCharacters(string gmn)
        {
            _FormatChecks(gmn, true);

            // Split off the provided check characters, recalculate them and ensure that they match
            string part = gmn.Substring(0, gmn.Length - 2);
            string suppliedChecks = gmn.Substring(gmn.Length - 2, 2);

            return CheckCharacters(part).Equals(suppliedChecks);
        }

        // Perform consistency checks on the GMN data
        private static void _FormatChecks(string input, bool complete)
        {
            int maxLength = complete ? 25 : 23;
            int minLength = complete ? 8 : 6;

            // Verify length
            if (input.Length < minLength)
                throw new GS1Exception("The input is too short.");
            if (input.Length > maxLength)
                throw new GS1Exception("The input is too long.");

            // Ensure that first five digits are numeric
            for (int i = 0; i < 5; i++)
            {
                if (!Char.IsDigit(input[i]))
                    throw new GS1Exception("The first five characters must be digits.");
            }

            // Verify that the remaining content is in the encodable character set
            for (int i = 5; i < input.Length; i++)
            {
                if (!cset82val.ContainsKey(input[i]))
                    throw new GS1Exception("Input contains an invalid character in position " + (i + 1) + ": " + input[i]);
            }

            return;
        }

    }


    /// <summary>
    /// A custom exception class to differentiate exceptions raised by the utility
    /// class from other sources of error.
    /// </summary>
    public class GS1Exception : Exception
    {
        /// <summary>
        /// Error constructor.
        /// </summary>
        /// <param name="message">
        /// Description of the error.
        /// </param>
        public GS1Exception(string message)
           : base(message)
        {
        }
    }

}
