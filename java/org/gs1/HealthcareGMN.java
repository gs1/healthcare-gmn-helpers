package org.gs1;

import java.util.Collections;
import java.util.Map;
import java.util.HashMap;

/**
 * Helper class that is both a demonstration and usable implementation of a
 * check character generator and verifier for a GS1 GMN that is used for
 * Regulated Healthcare medical devices that fall under the EU regulations EU
 * MDR 2017/745 and EU IVDR 2017/746, herein referred to as a "healthcare GMN".
 *
 * @author Copyright (c) 2019 GS1 AISBL.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 *
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

public final class HealthcareGMN {

    /**
     * Descending primes used as multipliers of each data character.
     */
    private final static short[] weights = new short[]
        {83,79,73,71,67,61,59,53,47,43,41,37,31,29,23,19,17,13,11,7,5,3,2};

    /**
     * GS1 AI encodable character set 82. Place in the string represents the
     * character value.
     */
    private final static String cset82 =
        "!\"%&'()*+,-./0123456789:;<=>?ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
        "_abcdefghijklmnopqrstuvwxyz";

    /**
     * Subset of the encodable character set used for the check character pair.
     */
    private final static String cset32 = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ";

    /**
     * Character to value map for cset82.
     */
    private final static Map<Character, Short> cset82value;

    /**
     * Character to value map for cset32.
     */
    private final static Map<Character, Short> cset32value;

    // Initialisation populates the cset82 and cset32 mappings
    static
    {
        Map<Character, Short> tmp = new HashMap<>();
        for (short i = 0; i < cset82.length(); i++)
            tmp.put(cset82.charAt(i), i);
        cset82value=Collections.unmodifiableMap(tmp);

        tmp = new HashMap<>();
        for (short i = 0; i < cset32.length(); i++)
            tmp.put(cset32.charAt(i), i);
        cset32value=Collections.unmodifiableMap(tmp);
    }

    private HealthcareGMN() {}

    /**
     * Calculates the check character pair for a given partial healthcare GMN.
     *
     * @param part a partial healthcare GMN.
     * @return check character pair.
     * @throws GS1Exception if the format of the given healthcare GMN is invalid.
     */
    public static String checkCharacters(String part)
        throws GS1Exception
    {
        _formatChecks(part, false);

        /*
         * The GMN check character pair calculation is performed here.
         *
         */

        // Characters are compared with the rightmost weights
        int offset = 23 - part.length();

	// Modulo 1021 sum of the products of the character values and their
	// corresponding weights
	int sum = 0;
        for (int i=0; i < part.length(); i++)
        {
                short c = cset82value.get(part.charAt(i));
                short w = weights[ offset + i ];
                sum += c * w;
        }
        sum %= 1021;

        // Split the 10-bit sum over two five-bit check characters
        return "" + cset32.charAt(sum / 32) + cset32.charAt(sum % 32);
    }

    /**
     * Complete a given partial healthcare GMN by appending the check character pair.
     *
     * @param part a partial healthcare GMN.
     * @return a complete healthcare GMN including the check character pair.
     * @throws GS1Exception if the format of the given healthcare GMN is invalid.
     */
    public static String addCheckCharacters(String part)
        throws GS1Exception
    {
        return part + checkCharacters(part);
    }

    /**
     * Verify that a given healthcare GMN has a correct check character pair.
     *
     * @param gmn a healthcare GMN.
     * @return true if the healthcare GMN is has a valid check character pair. Otherwise false.
     * @throws GS1Exception if the format of the given healthcare GMN is invalid.
     */
    public static boolean verifyCheckCharacters(String gmn)
        throws GS1Exception
    {
        _formatChecks(gmn, true);

        // Split off the provided check character pair, recalculate them and ensure
        // that they match
        String part = gmn.substring(0, gmn.length() - 2);
        String suppliedChecks = gmn.substring(gmn.length() - 2, gmn.length());

        return checkCharacters(part).equals(suppliedChecks);
    }

    /**
     * Indicate whether each character in a given GMN belongs to the appropriate character set for the character position.
     *
     * @param gmn a full or partial healthcare GMN.
     * @param complete true if a GMN is being provided complete with a check character pair. Otherwise false.
     * @return a boolean array matching each input character: true if the character belongs to the appropriate set. Otherwise false.
     */
    public static boolean[] goodCharacterPositions(String gmn, boolean complete)
    {
        boolean[] out = new boolean[gmn.length()];
        for (int i = 0; i < gmn.length(); i++)
        {

            // GMN begins with a GS1 Company Prefix which is at least five characters
            if (i < 5)
                out[i] = Character.isDigit(gmn.charAt(i));
            else if (!complete || i < gmn.length() - 2)
                out[i] = cset82value.containsKey(gmn.charAt(i));
            else  // For a complete GMN final two positions are check character pair
                out[i] = cset32value.containsKey(gmn.charAt(i));

        }
        return out;
    };

    // Perform some local consistency checks on the input
    private static void _formatChecks(String input, boolean complete)
        throws GS1Exception
    {
        int maxLength = complete ? 25 : 23;
        int minLength = complete ? 8 : 6;

        // Verify length
        if (input.length() < minLength)
            throw new GS1Exception("The input is too short. It should be at least " + minLength + " characters long" + ( complete ? "." : " excluding the check character pair." ) );
        if (input.length() > maxLength)
            throw new GS1Exception("The input is too long. It should be " + maxLength + " characters maximum" + ( complete ? "." : " excluding the check character pair." ) );

        // Verify that the content is in the correct encodable character set
        boolean[] goodCharacters = goodCharacterPositions(input, complete);
        for (int i = 0; i < input.length(); i++)
            if (!goodCharacters[i])
            {
                if (i < 5)
                    throw new GS1Exception("GMN starts with the GS1 Company Prefix. At least the first five characters must be digits.");
                else if (!complete || i < input.length() - 2)
                    throw new GS1Exception("Invalid character at position " + (i + 1) + ": " + input.charAt(i));
                else
                    throw new GS1Exception("Invalid check character at position " + (i + 1) + ": " + input.charAt(i));
            }

        return;
    }

}

