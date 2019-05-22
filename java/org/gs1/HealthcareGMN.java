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
 * @author (c) 2019 GS1 AISBL. All rights reserved.
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

    // Initialisation populates the cset82 mapping
    static
    {
        Map<Character, Short> tmp = new HashMap<>();
        for (short i = 0; i < cset82.length(); i++)
            tmp.put(cset82.charAt(i), i);
        cset82value=Collections.unmodifiableMap(tmp);
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

    // Perform some local consistency checks on the input
    private static void _formatChecks(String input, boolean complete)
        throws GS1Exception
    {
        int maxLength = complete ? 25 : 23;
        int minLength = complete ? 8 : 6;

        // Verify length
        if (input.length() < minLength)
            throw new GS1Exception("The input is too short. It should be at least 6 characters long excluding the check character pair.");
        if (input.length() > maxLength)
            throw new GS1Exception("The input is too long. It should be 23 characters maximum excluding the check character pair.");

        // Ensure that first five digits are numeric
        for (int i = 0; i < 5; i++)
        {
            if (!Character.isDigit(input.charAt(i)))
                throw new GS1Exception("GMN starts with the GS1 Company Prefix. At least the first five characters must be digits.");
        }

        // Verify that the remaining content is in the encodable character set
        for (int i = 5; i < input.length(); i++)
        {
            if (!cset82value.containsKey(input.charAt(i)))
                throw new GS1Exception("Invalid character at position " +
                    (i + 1) + ": " + input.charAt(i));
        }

        return;
    }

}

