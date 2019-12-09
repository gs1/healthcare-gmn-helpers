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

var HealthcareGMN = require('./healthcaregmn');

test('verifyCheckCharacters_UsingExampleFromGenSpecs', () => {
  expect(HealthcareGMN.verifyCheckCharacters("1987654Ad4X4bL5ttr2310c2K")).toBe(true);
});

test('checkCharacters_UsingExampleFromGenSpecs', () => {
  expect(HealthcareGMN.checkCharacters("1987654Ad4X4bL5ttr2310c")).toBe("2K");
});

test('addCheckCharacters_UsingExampleFromGenSpecs', () => {
  expect(HealthcareGMN.addCheckCharacters("1987654Ad4X4bL5ttr2310c")).toBe("1987654Ad4X4bL5ttr2310c2K");
});

test('verifyCheckCharacters_InvalidCheck1', () => {
  expect(HealthcareGMN.verifyCheckCharacters("1987654Ad4X4bL5ttr2310cXK")).toBe(false);
});

test('verifyCheckCharacters_InvalidCheck2', () => {
  expect(HealthcareGMN.verifyCheckCharacters("1987654Ad4X4bL5ttr2310c2X")).toBe(false);
});

test('verifyCheckCharacters_First5NotNumericAtStart', () => {
  expect( () => HealthcareGMN.verifyCheckCharacters("X987654Ad4X4bL5ttr2310c2K")).toThrow("must be digits");
});

test('verifyCheckCharacters_First5NotNumericAtEnd', () => {
  expect( () => HealthcareGMN.verifyCheckCharacters("1987X54Ad4X4bL5ttr2310c2K")).toThrow("must be digits");
});

test('verifyCheckCharacters_InvalidCharacterNearStart', () => {
  expect( () => HealthcareGMN.verifyCheckCharacters("198765£Ad4X4bL5ttr2310c2K")).toThrow("Invalid character");
});

test('verifyCheckCharacters_InvalidCharacterAtEndModel', () => {
  expect( () => HealthcareGMN.verifyCheckCharacters("1987654Ad4X4bL5ttr2310£2K")).toThrow("Invalid character");
});

test('addCheckCharacters_InvalidCharacterAtEndModel', () => {
  expect( () => HealthcareGMN.addCheckCharacters("1987654Ad4X4bL5ttr2310£")).toThrow("Invalid character");
});

test('verifyCheckCharacters_InvalidCharacterAtCheck1', () => {
  expect( () => HealthcareGMN.verifyCheckCharacters("1987654Ad4X4bL5ttr2310cxK")).toThrow("Invalid check character");
});

test('verifyCheckCharacters_InvalidCharacterAtCheck2', () => {
  expect( () => HealthcareGMN.verifyCheckCharacters("1987654Ad4X4bL5ttr2310c2x")).toThrow("Invalid check character");
});

test('verifyCheckCharacters_TooShort', () => {
  expect( () => HealthcareGMN.verifyCheckCharacters("12345XX")).toThrow("too short");
});

test('verifyCheckCharacters_Shortest', () => {
  expect(HealthcareGMN.verifyCheckCharacters("12345ANJ")).toBe(true);
});

test('verifyCheckCharacters_TooLong', () => {
  expect( () => HealthcareGMN.verifyCheckCharacters("123456789012345678901234XX")).toThrow("too long");
});

test('verifyCheckCharacters_Longest', () => {
  expect(HealthcareGMN.verifyCheckCharacters("12345678901234567890123NT")).toBe(true);
});

test('checkCharacters_TooShort', () => {
  expect( () => HealthcareGMN.checkCharacters("12345")).toThrow("too short");
});

test('checkCharacters_Shortest', () => {
  expect(HealthcareGMN.checkCharacters("12345A")).toBe("NJ");
});

test('checkCharacters_TooLong', () => {
  expect( () => HealthcareGMN.checkCharacters("123456789012345678901234")).toThrow("too long");
});

test('checkCharacters_Longest', () => {
  expect(HealthcareGMN.checkCharacters("12345678901234567890123")).toBe("NT");
});

test('verifyCheckCharacters_AllCSET82', () => {
  expect(HealthcareGMN.verifyCheckCharacters("12345_ABCDEFGHIJKLMCP")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("12345_NOPQRSTUVWXYZDN")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("12345_abcdefghijklmN3")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("12345_nopqrstuvwxyzP2")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("12345_!\"%&'()*+,-./LC")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("12345_0123456789:;<=>?62")).toBe(true);
});

test('verifyCheckCharacters_AllCSET32', () => {
  expect(HealthcareGMN.verifyCheckCharacters("7907665Bm8v2AB")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("97850l6KZm0yCD")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("225803106GSpEF")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("149512464PM+GH")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("62577B8fRG7HJK")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("515942070CYxLM")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("390800494sP6NP")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("386830132uO+QR")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("53395376X1:nST")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("957813138Sb6UV")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("530790no0qOgWX")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("62185314IvwmYZ")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("23956qk1&dB!23")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("794394895ic045")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("57453Uq3qA<H67")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("62185314IvwmYZ")).toBe(true);
  expect(HealthcareGMN.verifyCheckCharacters("0881063PhHvY89")).toBe(true);
});

test('verifyCheckCharacters_MinimumIntermediateSum', () => {
  expect(HealthcareGMN.verifyCheckCharacters("00000!HV")).toBe(true);
});

test('verifyCheckCharacters_MaximumIntermediateSum', () => {
  expect(HealthcareGMN.verifyCheckCharacters("99999zzzzzzzzzzzzzzzzzzT2")).toBe(true);
});

test('verifyCheckCharactersGcpModelChecks_UsingExampleFromGenSpecs', () => {
  expect(HealthcareGMN.verifyCheckCharactersGcpModelChecks("1987654","Ad4X4bL5ttr2310c","2K")).toBe(true);
});

test('checkCharactersGcpModel_UsingExampleFromGenSpecs', () => {
  expect(HealthcareGMN.checkCharactersGcpModel("1987654","Ad4X4bL5ttr2310c")).toBe("2K");
});

test('addCheckCharactersGcpModel_UsingExampleFromGenSpecs', () => {
  expect(HealthcareGMN.addCheckCharactersGcpModel("1987654","Ad4X4bL5ttr2310c")).toBe("1987654Ad4X4bL5ttr2310c2K");
});

test('verifyCheckCharactersGcpModelChecks_InvalidCheck1', () => {
  expect(HealthcareGMN.verifyCheckCharactersGcpModelChecks("1987654","Ad4X4bL5ttr2310c","XK")).toBe(false);
});

test('verifyCheckCharactersGcpModelChecks_InvalidCheck2', () => {
  expect(HealthcareGMN.verifyCheckCharactersGcpModelChecks("1987654","Ad4X4bL5ttr2310c","2X")).toBe(false);
});

test('verifyCheckCharactersGcpModelChecks_CheckTooShort', () => {
  expect( () => HealthcareGMN.verifyCheckCharactersGcpModelChecks("1987654","Ad4X4bL5ttr2310c","3")).toThrow("check must be 2 characters long");
});

test('verifyCheckCharactersGcpModelChecks_CheckTooLong', () => {
  expect( () => HealthcareGMN.verifyCheckCharactersGcpModelChecks("1987654","Ad4X4bL5ttr2310c","2KX")).toThrow("check must be 2 characters long");
});

test('verifyCheckCharactersGcpModelChecks_GcpTooShort', () => {
  expect( () => HealthcareGMN.verifyCheckCharactersGcpModelChecks("1234","Ad4X4bL5ttr2310c","XX")).toThrow("GS1 Company Prefix is too short");
});

test('verifyCheckCharactersGcpModelChecks_GcpNotTooShort', () => {
  expect(HealthcareGMN.verifyCheckCharactersGcpModelChecks("12345","Ad4X4bL5ttr2310c","66")).toBe(true);
});

test('verifyCheckCharactersGcpModelChecks_GcpTooLong', () => {
  expect( () => HealthcareGMN.verifyCheckCharactersGcpModelChecks("1234567890123","Ad4X4bL5","XX")).toThrow("GS1 Company Prefix is too long");
});

test('verifyCheckCharactersGcpModelChecks_GcpNotTooLong', () => {
  expect(HealthcareGMN.verifyCheckCharactersGcpModelChecks("123456789012","Ad4X4bL5ttr","3R")).toBe(true);
});

test('verifyCheckCharactersGcpModelChecks_GcpNotNumeric', () => {
  expect( () => HealthcareGMN.verifyCheckCharactersGcpModelChecks("198765A","Ad4X4bL5ttr2310c","XX")).toThrow("GS1 Company Prefix must only contain digits");
});

test('verifyCheckCharactersGcpModelChecks_ModelEmpty', () => {
  expect( () => HealthcareGMN.verifyCheckCharactersGcpModelChecks("1987654","","3T")).toThrow("model reference must contain at least one character");
});

test('verifyCheckCharactersGcpModelChecks_OversizeShortestGCP', () => {
  expect( () => HealthcareGMN.verifyCheckCharactersGcpModelChecks("12345","6789012345678901234","XX")).toThrow("input is too long");
});

test('verifyCheckCharactersGcpModelChecks_NotOversizeShortestGCP', () => {
  expect(HealthcareGMN.verifyCheckCharactersGcpModelChecks("12345","678901234567890123","NT")).toBe(true);
});

test('verifyCheckCharactersGcpModelChecks_OversizeLongestGCP', () => {
  expect( () => HealthcareGMN.verifyCheckCharactersGcpModelChecks("123456789012","345678901234","XX")).toThrow("input is too long");
});

test('verifyCheckCharactersGcpModelChecks_NotOversizeLongestGCP', () => {
  expect(HealthcareGMN.verifyCheckCharactersGcpModelChecks("123456789012","34567890123","NT")).toBe(true);
});
