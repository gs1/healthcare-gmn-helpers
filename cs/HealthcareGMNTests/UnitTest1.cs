using System;
using Xunit;

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
using static GS1.HealthcareGMN;

namespace HealthcareGMNTests
{
    public class UnitTest1
    {
        [Fact]
        public void VerifyCheckCharacters_UsingExampleFromGenSpecs()
        {
            Assert.True(VerifyCheckCharacters("1987654Ad4X4bL5ttr2310c2K"));
        }

        [Fact]
        public void CheckCharacters_UsingExampleFromGenSpecs()
        {
            Assert.Equal("2K",CheckCharacters("1987654Ad4X4bL5ttr2310c"));
        }

        [Fact]
        public void AddCheckCharacters_UsingExampleFromGenSpecs()
        {
            Assert.Equal("1987654Ad4X4bL5ttr2310c2K", AddCheckCharacters("1987654Ad4X4bL5ttr2310c"));
        }

        [Fact]
        public void VerifyCheckCharacters_InvalidCheck1()
        {
            Assert.False(VerifyCheckCharacters("1987654Ad4X4bL5ttr2310cXK"));
        }

        [Fact]
        public void VerifyCheckCharacters_InvalidCheck2()
        {
            Assert.False(VerifyCheckCharacters("1987654Ad4X4bL5ttr2310c2X"));
        }

        [Fact]
        public void VerifyCheckCharacters_First5NotNumericAtStart()
        {
            Exception e = Assert.ThrowsAny<Exception>(() => VerifyCheckCharacters("X987654Ad4X4bL5ttr2310c2K"));
            Assert.Contains("must be digits", e.Message.ToLower());
        }

        [Fact]
        public void VerifyCheckCharacters_First5NotNumericAtEnd()
        {
            Exception e = Assert.ThrowsAny<Exception>(() => VerifyCheckCharacters("1987X54Ad4X4bL5ttr2310c2K"));
            Assert.Contains("must be digits", e.Message.ToLower());
        }

        [Fact]
        public void VerifyCheckCharacters_InvalidCharacterNearStart()
        {
            Exception e = Assert.ThrowsAny<Exception>(() => VerifyCheckCharacters("198765£Ad4X4bL5ttr2310c2K"));
            Assert.Contains("invalid character", e.Message.ToLower());
        }

        [Fact]
        public void VerifyCheckCharacters_InvalidCharacterAtEnd()
        {
            Exception e = Assert.ThrowsAny<Exception>(() => VerifyCheckCharacters("1987654Ad4X4bL5ttr2310c2£"));
            Assert.Contains("invalid character", e.Message.ToLower());
        }

        [Fact]
        public void VerifyCheckCharacters_TooShort()
        {
            Exception e = Assert.ThrowsAny<Exception>(() => VerifyCheckCharacters("12345XX"));
            Assert.Contains("too short", e.Message.ToLower());
        }

        [Fact]
        public void VerifyCheckCharacters_Shortest()
        {
            Assert.True(VerifyCheckCharacters("12345ANJ"));
        }

        [Fact]
        public void VerifyCheckCharacters_TooLong()
        {
            Exception e = Assert.ThrowsAny<Exception>(() => VerifyCheckCharacters("123456789012345678901234XX"));
            Assert.Contains("too long", e.Message.ToLower());
        }

        [Fact]
        public void VerifyCheckCharacters_Longest()
        {
            Assert.True(VerifyCheckCharacters("12345678901234567890123NT"));
        }

        [Fact]
        public void CheckCharacters_TooShort()
        {
            Exception e = Assert.ThrowsAny<Exception>(() => CheckCharacters("12345"));
            Assert.Contains("too short", e.Message.ToLower());
        }

        [Fact]
        public void CheckCharacters_Shortest()
        {
            Assert.Equal("NJ",CheckCharacters("12345A"));
        }

        [Fact]
        public void CheckCharacters_TooLong()
        {
            Exception e = Assert.ThrowsAny<Exception>(() => CheckCharacters("123456789012345678901234"));
            Assert.Contains("too long", e.Message.ToLower());
        }

        [Fact]
        public void CheckCharacters_Longest()
        {
            Assert.Equal("NT",CheckCharacters("12345678901234567890123"));
        }

        [Fact]
        public void VerifyCheckCharacters_AllCSET82()
        {
            // The aim here is to prevent regressions due to modifications of the CSET 82 characters
            Assert.True(VerifyCheckCharacters("12345_ABCDEFGHIJKLMCP"));
            Assert.True(VerifyCheckCharacters("12345_NOPQRSTUVWXYZDN"));
            Assert.True(VerifyCheckCharacters("12345_abcdefghijklmN3"));
            Assert.True(VerifyCheckCharacters("12345_nopqrstuvwxyzP2"));
            Assert.True(VerifyCheckCharacters("12345_!\"%&'()*+,-./LC"));
            Assert.True(VerifyCheckCharacters("12345_0123456789:;<=>?62"));
        }

        [Fact]
        public void VerifyCheckCharacters_AllCSET32()
        {
            // The aim here is to prevent regressions due to modifications of the CSET 32 characters
            Assert.True(VerifyCheckCharacters("7907665Bm8v2AB"));
            Assert.True(VerifyCheckCharacters("97850l6KZm0yCD"));
            Assert.True(VerifyCheckCharacters("225803106GSpEF"));
            Assert.True(VerifyCheckCharacters("149512464PM+GH"));
            Assert.True(VerifyCheckCharacters("62577B8fRG7HJK"));
            Assert.True(VerifyCheckCharacters("515942070CYxLM"));
            Assert.True(VerifyCheckCharacters("390800494sP6NP"));
            Assert.True(VerifyCheckCharacters("386830132uO+QR"));
            Assert.True(VerifyCheckCharacters("53395376X1:nST"));
            Assert.True(VerifyCheckCharacters("957813138Sb6UV"));
            Assert.True(VerifyCheckCharacters("530790no0qOgWX"));
            Assert.True(VerifyCheckCharacters("62185314IvwmYZ"));
            Assert.True(VerifyCheckCharacters("23956qk1&dB!23"));
            Assert.True(VerifyCheckCharacters("794394895ic045"));
            Assert.True(VerifyCheckCharacters("57453Uq3qA<H67"));
            Assert.True(VerifyCheckCharacters("62185314IvwmYZ"));
            Assert.True(VerifyCheckCharacters("0881063PhHvY89"));
        }

        [Fact]
        public void VerifyCheckCharacters_MinimumIntermediateSum()
        {
            Assert.True(VerifyCheckCharacters("00000!HV"));
        }

        [Fact]
        public void VerifyCheckCharacters_MaximumIntermediateSum()
        {
            Assert.True(VerifyCheckCharacters("99999zzzzzzzzzzzzzzzzzzT2"));
        }

    }
}
