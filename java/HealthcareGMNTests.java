import org.junit.Test;
import static org.junit.Assert.*;

/*
 * These are unit tests for the public methods of the helper API.
 *
 * You should rerun these tests if you modify the library in any way.
 *
 * Alternatively, if you reimplement the library then you can simply run the
 * tests against your own code if the method names are compatible (or you
 * provide wrappers). Simply modify the static import immediately below.
 *
 */
import org.gs1.*;
import static org.gs1.HealthcareGMN.*;

public class HealthcareGMNTests
{

        @Test
        public void verifyCheckCharacters_UsingExampleFromGenSpecs() throws Exception
        {
            assertTrue(verifyCheckCharacters("1987654Ad4X4bL5ttr2310c2K"));
        }

        @Test
        public void checkCharacters_UsingExampleFromGenSpecs() throws Exception
        {
            assertEquals("2K",checkCharacters("1987654Ad4X4bL5ttr2310c"));
        }

        @Test
        public void addCheckCharacters_UsingExampleFromGenSpecs() throws Exception
        {
            assertEquals("1987654Ad4X4bL5ttr2310c2K", addCheckCharacters("1987654Ad4X4bL5ttr2310c"));
        }

        @Test
        public void verifyCheckCharacters_InvalidCheck1() throws Exception
        {
            assertFalse(verifyCheckCharacters("1987654Ad4X4bL5ttr2310cXK"));
        }

        @Test
        public void verifyCheckCharacters_InvalidCheck2() throws Exception
        {
            assertFalse(verifyCheckCharacters("1987654Ad4X4bL5ttr2310c2X"));
        }

        @Test(expected = Exception.class)
        public void verifyCheckCharacters_First5NotNumericiAtStart() throws Exception
        {
            verifyCheckCharacters("X987654Ad4X4bL5ttr2310c2K");
        }

        @Test(expected = Exception.class)
        public void verifyCheckCharacters_First5NotNumericAtEnd() throws Exception
        {
            verifyCheckCharacters("1987X54Ad4X4bL5ttr2310c2K");
        }

        @Test(expected = Exception.class)
        public void verifyCheckCharacters_InvalidCharacterNearStart() throws Exception
        {
            verifyCheckCharacters("19876£4Ad4X4bL5ttr2310c2K");
        }

        @Test(expected = Exception.class)
        public void verifyCheckCharacters_InvalidCharacterAtEnd() throws Exception
        {
            verifyCheckCharacters("1987654Ad4X4bL5ttr2310c2£");
        }

        @Test(expected = Exception.class)
        public void verifyCheckCharacters_TooShort() throws Exception
        {
            verifyCheckCharacters("12345XX");
        }

        @Test
        public void verifyCheckCharacters_Shortest() throws Exception
        {
            assertTrue(verifyCheckCharacters("12345ANJ"));
        }

        @Test(expected = Exception.class)
        public void verifyCheckCharacters_TooLong() throws Exception
        {
            verifyCheckCharacters("123456789012345678901234XX");
        }


        @Test
        public void verifyCheckCharacters_Longest() throws Exception
        {
            assertTrue(verifyCheckCharacters("12345678901234567890123NT"));
        }

        @Test(expected = Exception.class)
        public void checkCharacters_TooShort() throws Exception
        {
            checkCharacters("12345");
        }

        @Test
        public void checkCharacters_Shortest() throws Exception
        {
            assertEquals("NJ",checkCharacters("12345A"));
        }

        @Test(expected = Exception.class)
        public void checkCharacters_TooLong() throws Exception
        {
            checkCharacters("123456789012345678901234");
        }

        @Test
        public void checkCharacters_Longest() throws Exception
        {
            assertEquals("NT",checkCharacters("12345678901234567890123"));
        }

        @Test
        public void verifyCheckCharacters_AllCSET82() throws Exception
        {
            // The aim here is to prevent regressions due to modifications of the CSET 82 characters
            assertTrue(verifyCheckCharacters("12345_ABCDEFGHIJKLMCP"));
            assertTrue(verifyCheckCharacters("12345_NOPQRSTUVWXYZDN"));
            assertTrue(verifyCheckCharacters("12345_abcdefghijklmN3"));
            assertTrue(verifyCheckCharacters("12345_nopqrstuvwxyzP2"));
            assertTrue(verifyCheckCharacters("12345_!\"%&'()*+,-./LC"));
            assertTrue(verifyCheckCharacters("12345_0123456789:;<=>?62"));
        }

        @Test
        public void verifyCheckCharacters_AllCSET32() throws Exception
        {
            // The aim here is to prevent regressions due to modifications of the CSET 32 characters
            assertTrue(verifyCheckCharacters("7907665Bm8v2AB"));
            assertTrue(verifyCheckCharacters("97850l6KZm0yCD"));
            assertTrue(verifyCheckCharacters("225803106GSpEF"));
            assertTrue(verifyCheckCharacters("149512464PM+GH"));
            assertTrue(verifyCheckCharacters("62577B8fRG7HJK"));
            assertTrue(verifyCheckCharacters("515942070CYxLM"));
            assertTrue(verifyCheckCharacters("390800494sP6NP"));
            assertTrue(verifyCheckCharacters("386830132uO+QR"));
            assertTrue(verifyCheckCharacters("53395376X1:nST"));
            assertTrue(verifyCheckCharacters("957813138Sb6UV"));
            assertTrue(verifyCheckCharacters("530790no0qOgWX"));
            assertTrue(verifyCheckCharacters("62185314IvwmYZ"));
            assertTrue(verifyCheckCharacters("23956qk1&dB!23"));
            assertTrue(verifyCheckCharacters("794394895ic045"));
            assertTrue(verifyCheckCharacters("57453Uq3qA<H67"));
            assertTrue(verifyCheckCharacters("62185314IvwmYZ"));
            assertTrue(verifyCheckCharacters("0881063PhHvY89"));
        }

        @Test
        public void verifyCheckCharacters_MinimumIntermediateSum() throws Exception
        {
            assertTrue(verifyCheckCharacters("00000!HV"));
        }

        @Test
        public void verifyCheckCharacters_MaximumIntermediateSum() throws Exception
        {
            assertTrue(verifyCheckCharacters("99999zzzzzzzzzzzzzzzzzzT2"));
        }

}
