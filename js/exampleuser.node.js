/*
 * Example user of the helper library.
 *
 * The associated library is a check character generator and verifier for a GS1
 * GMN that is used for Regulated Healthcare medical devices that fall under
 * the EU regulations EU MDR 2017/745 and EU IVDR 2017/746. Herein refered to
 * as a "healthcare GMN".
 *
 * (c) 2019 GS1 AISBL. All rights reserved.
 *
 */

"use strict";

const HealthcareGMN = require('./healthcaregmn');

const fs = require('fs');
const rl = require('readline-sync');


/*
 * User has provided commandline arguments. We tuck this code out of
 * the way for clarity and demonstrate some simpler cases first.
 *
 */
if (process.argv.length > 2)
    processUserInput(process.argv);


/*
 * Non-interactive demonstration
 *
 */
console.log("\nOutput from non-interactive demonstration");
console.log("*****************************************\n");

var gmn;
var partialGMN;
var complete;
var checkCharacters;
var valid;

/*
 * Example: org.gs1.HealthcareGMN.verifyCheckCharacters
 *
 * Verifying the check characters of a healthcare GMN.
 *
 */
try
{

    gmn = "1987654Ad4X4bL5ttr2310c2K";      // Valid healthcare GMN based on example from the Gen Specs
    // gmn = "1987654Ad4X4bL5ttr2310cZZ";   // Invalid: Bad check digits

    // Examples that raise exceptions:
    //
    // gmn = "1987654Ad4X4bL5ttr2310c2KZ";  // Exception: Too long
    // gmn = "12345ABCZZ";                  // Exception: Too short
    // gmn = "ABC7654Ad4X4bL5ttr2310cZZ";   // Exception: Doesn't start with five digits
    // gmn = "12345£££d4X4bL5ttr2310cZZ";   // Exception: Contains characters outside of CSET 82

    /* Call the org.gs1.HealthcareGMN.verifyCheckCharacters helper */
    valid = HealthcareGMN.verifyCheckCharacters(gmn);

    if (valid)
    {
        console.log("This healthcare GMN has correct check characters: " + gmn);
    }
    else
    {
        console.log("This healthcare GMN has incorrect check characters: " + gmn);
    }
    console.log();

}
catch (e)
{
    console.error("Something went wrong: " + e);
    process.exit(1);
}


/*
 * Example: org.gs1.HealthcareGMN.addCheckCharacters
 *
 * Adding the check characters to an incomplete healthcare GMN.
 *
 */
try
{

    partialGMN = "1987654Ad4X4bL5ttr2310c";      // Based on example from the Gen Specs

    // Examples that raise exceptions:
    //
    // String partialGMN = "1987654Ad4X4bL5ttr2310cX";  // Exception: Too long
    // String partialGMN = "12345ABC";                  // Exception: Too short
    // String partialGMN = "ABC7654Ad4X4bL5ttr2310c";   // Exception: Doesn't start with five digits
    // String partialGMN = "12345£££d4X4bL5ttr2310c";   // Exception: Contains characters outside of CSET 82

    /* Call the org.gs1.HealthcareGMN.addCheckCharacters helper */
    gmn = HealthcareGMN.addCheckCharacters(partialGMN);

    console.log("Partial:  " + partialGMN);
    console.log("Full GMN: " + gmn);
    console.log();

}
catch (e)
{
    console.error("Something went wrong: "+e);
    process.exit(1);
}


/*
 * Example: org.gs1.HealthcareGMN.checkCharacters
 *
 * Returning just the check characters, then completing the healthcare GMN
 *
 */
try
{

    partialGMN = "1987654Ad4X4bL5ttr2310c";      // Based on example from the Gen Specs

    /* Call the org.gs1.HealthcareGMN.checkCharacters helper */
    checkCharacters = HealthcareGMN.checkCharacters(partialGMN);

    console.log("Partial:  " + partialGMN);
    console.log("Checks:   " + checkCharacters);

    // Contatenate to produce the complete healthcare GMN
    gmn = partialGMN + checkCharacters;
    console.log("Full GMN: " + gmn);

    console.log();

}
catch (e)
{
    console.error("Something went wrong: " + e);
    process.exit(1);
}


/*
 * Interactive demonstration
 *
 */
console.log("\nInteractive demonstration");
console.log("*************************");

for (;;)
{

    console.log("\nPlease select an option:\n");
    console.log("  c  - Complete a partial GMN by adding check digits");
    console.log("  v  - Verify the check digits of a complete GMN");
    console.log("  cf - Complete partial GMNs supplied on each line of a file");
    console.log("  vf - Verify complete GMNs supplied on each line of a file");
    console.log("  q  - Quit");

    var opt = rl.question("\nEnter option (c/v/cf/vf/q)? ");

    // Quit
    if (opt === "q")
        break;

    // Verify a GMN
    if (opt ===  "v")
    {
        try
        {
            gmn = rl.question("\nPlease supply a GMN to verify: ");
            valid = HealthcareGMN.verifyCheckCharacters(gmn);
            console.log("Outcome: " + (valid ? "*** Valid ***" : "*** Not valid ***") );
        }
        catch (e)
        {
            console.log("Error with input: " + e);
        }
        continue;
    }

    // Complete a partial GMN
    if (opt === "c")
    {
        try
        {
            gmn = rl.question("\nPlease supply a partial GMN to complete: ");
            complete = HealthcareGMN.addCheckCharacters(gmn);
            console.log("Complete GMN: " + complete);
        }
        catch (e)
        {
            console.log("Error with input: " + e);
        }
        continue;
    }

    // Operations line by line on a file
    if (opt === "cf" || opt === "vf")
    {
        try
        {
            var filename = rl.question("\nPlease supply a filename: ");
            fs.readFileSync(filename).toString().match(/^.+$/gm).forEach(function (line) {
                var out;
                try
                {
                    if (opt === "cf")
                    {
                        out = HealthcareGMN.checkCharacters(line);
                    }
                    else
                    {  // vf
                        out = HealthcareGMN.verifyCheckCharacters(line) ? "*** Valid ***" : "*** Not valid ***";
                    }
                }
                catch (e)
                {
                    out = e;
                }
                console.log(line + " : " + out);
            });
        }
        catch (e)
        {
            console.log(e);
        }
        continue;
    }

}


/*
 * Terse demonstration of processing commandline input provided by the user
 *
 */
function processUserInput(args)
{

    args.shift();
    args.shift();

    if (args.length != 2 || ((args[0] !== "verify") && (args[0] !== "complete")))
    {
        console.log("\nIncorrect arguments.\n");
        console.log("Usage: java ExampleUser {verify|complete} gmn_data\n");
        process.exit(1);
    }

    try
    {
        if (args[0] === "verify")
        {
            var valid = HealthcareGMN.verifyCheckCharacters(args[1]);
            console.log("The check characters are " + (valid ? "valid" : "NOT valid"));
            process.exit(valid ? 1:0);
        }
        else   // complete
        {
            console.log(HealthcareGMN.addCheckCharacters(args[1]));
            process.exit(0);
        }
    }
    catch (e)
    {
        console.error("Error: " + e);
        process.exit(1);
    }

}
