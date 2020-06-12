/*
 * Healthcare GMN check digit generator and verifier example user.
 *
 * The associated library is a check character generator and verifier for a GS1
 * GMN that is used for Regulated Healthcare medical devices that fall under
 * the EU regulations EU MDR 2017/745 and EU IVDR 2017/746. Herein refered to
 * as a "healthcare GMN".
 *
 * Copyright (c) 2019 GS1 AISBL.
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

using System;
using GS1;  // Add a reference to the utility class to your project

namespace ExampleUser
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
             * User has provided commandline arguments. We tuck this code out of
             * the way for clarity and demonstrate some simpler cases first.
             *
             */
            if (args.Length > 0)
                processUserInput(args);


            /*
             * Non-interactive demonstration
             *
             */
            Console.WriteLine("\nOutput from non-interactive demonstration");
            Console.WriteLine("*****************************************\n");

            /*
             * Example: GS1.HealthcareGMN.VerifyCheckCharacters
             *
             * Verifying the check characters of a healthcare GMN.
             *
             */
            try
            {

                string gmn = "1987654Ad4X4bL5ttr2310c2K";      // Valid GMN based on example from the Gen Specs
                // string gmn = "1987654Ad4X4bL5ttr2310cZZ";   // Invalid: Bad check digits

                // Examples that raise exceptions:
                //
                // string gmn = "1987654Ad4X4bL5ttr2310c2KZ";  // Exception: Too long
                // string gmn = "12345AB";                     // Exception: Too short
                // string gmn = "ABC7654Ad4X4bL5ttr2310cZZ";   // Exception: Doesn't start with five digits
                // string gmn = "12345£££d4X4bL5ttr2310cZZ";   // Exception: Contains characters outside of CSET 82

                /* Call the GS1.Healthcare.VerifyCheckCharacters helper */
                bool valid = HealthcareGMN.VerifyCheckCharacters(gmn);

                if (valid)
                {
                    Console.WriteLine("This healthcare GMN has correct check characters: " + gmn);
                }
                else
                {
                    Console.WriteLine("This healthcare GMN has incorrect check characters: " + gmn);
                }
                Console.WriteLine();

            }
            catch (GS1Exception e)
            {
                Console.Error.WriteLine("Something went wrong: " + e.Message);
                System.Environment.Exit(1);
            }


            /*
             * Example: GS1.HealthcareGMN.AddCheckCharacters
             *
             * Adding the check characters to an incomplete healthcare GMN.
             *
             */
            try
            {

                string partialGMN = "1987654Ad4X4bL5ttr2310c";      // Based on example from the Gen Specs

                // Examples that raise exceptions:
                //
                // string partialGMN = "1987654Ad4X4bL5ttr2310cX";  // Exception: Too long
                // string partialGMN = "12345";                     // Exception: Too short
                // string partialGMN = "ABC7654Ad4X4bL5ttr2310c";   // Exception: Doesn't start with five digits
                // string partialGMN = "12345£££d4X4bL5ttr2310c";   // Exception: Contains characters outside of CSET 82

                /* Call the GS1.HealthcareGMN.AddCheckCharacters helper */
                string gmn = HealthcareGMN.AddCheckCharacters(partialGMN);

                Console.WriteLine("Partial:  " + partialGMN);
                Console.WriteLine("Full GMN: " + gmn);
                Console.WriteLine();

            }
            catch (GS1Exception e)
            {
                Console.Error.WriteLine("Something went wrong: "+e.Message);
                System.Environment.Exit(1);
            }


            /*
             * Example: GS1.HealthcareGMN.CheckCharacters
             *
             * Returning just the check characters, then completing the healthcare GMN
             *
             */
            try
            {

                string partialGMN = "1987654Ad4X4bL5ttr2310c";      // Based on example from the Gen Specs

                /* Call the GS1.HealthCareGMN.CheckCharacters helper */
                string checkCharacters = HealthcareGMN.CheckCharacters(partialGMN);

                Console.WriteLine("Partial:  " + partialGMN);
                Console.WriteLine("Checks:   " + checkCharacters);

                // Contatenate to produce the complete healthcare GMN
                string gmn = partialGMN + checkCharacters;
                Console.WriteLine("Full healthcare GMN: " + gmn);

                Console.WriteLine();

            }
            catch (GS1Exception e)
            {
                Console.Error.WriteLine("Something went wrong: " + e.Message);
                System.Environment.Exit(1);
            }


            /*
             * Interactive demonstration
             *
             */
            Console.WriteLine("\nInteractive demonstration");
            Console.WriteLine("*************************");

            while (true) {

                Console.WriteLine("\nPlease select an option:\n");
                Console.WriteLine("  c  - Complete a partial healthcare GMN by adding check digits");
                Console.WriteLine("  v  - Verify the check digits of a complete healthcare GMN");
                Console.WriteLine("  cf - Complete partial healthcare GMNs supplied on each line of a file");
                Console.WriteLine("  vf - Verify complete healthcare GMNs supplied on each line of a file");
                Console.WriteLine("  q  - Quit");
                Console.Write("\nEnter option (c/v/cf/vf/q)? ");
                string opt = Console.ReadLine();

                // Quit
                if (opt.Equals("q"))
                    break;

                // Verify a healthcare GMN
                if (opt.Equals("v"))
                {
                    try
                    {
                        Console.Write("\nPlease supply a healthcare GMN: ");
                        String gmn = Console.ReadLine();
                        bool valid = HealthcareGMN.VerifyCheckCharacters(gmn);
                        Console.WriteLine("Outcome: " + (valid ? "*** Valid ***" : "*** Not valid ***"));
                    }
                    catch (GS1Exception e)
                    {
                        Console.WriteLine("Error with input: " + e.Message);
                    }
                    continue;
                }

                // Complete a partial healthcare GMN
                if (opt.Equals("c"))
                {
                    try
                    {
                        Console.Write("\nPlease supply a partial healthcare GMN to complete: ");
                        String gmn = Console.ReadLine();
                        String complete = HealthcareGMN.AddCheckCharacters(gmn);
                        Console.WriteLine("Complete healthcare GMN: " + complete);
                    }
                    catch (GS1Exception e)
                    {
                        Console.WriteLine("Error with input: " + e.Message);
                    }
                    continue;
                }

                // Operations line by line on a file
                if (opt.Equals("cf") || opt.Equals("vf"))
                {
                    try
                    {
                        Console.Write("\nPlease supply a filename: ");
                        string filename = Console.ReadLine();
                        System.IO.StreamReader reader = new System.IO.StreamReader(filename);
                        string linein, lineout;
                        while ( (linein = reader.ReadLine()) != null )
                        {
                            try
                            {
                                if (opt.Equals("cf"))
                                {
                                    lineout = HealthcareGMN.CheckCharacters(linein);
                                }
                                else
                                {  // vf
                                    lineout = HealthcareGMN.VerifyCheckCharacters(linein) ? "*** Valid ***" : "*** Not valid ***";
                                }
                            }
                            catch (GS1Exception e)
                            {
                                lineout = e.Message;
                            }
                            Console.WriteLine(linein + " : " + lineout);
                        }
                        reader.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    continue;
                }

            }

            return;

        }


        /*
         * Terse demonstration of processing commandline input provided by the user
         *
         */
        private static void processUserInput(string[] args) {

            if (args.Length != 2 || (!args[0].Equals("verify") && !args[0].Equals("complete"))) {
                Console.WriteLine("\nIncorrect arguments.\n");
                Console.WriteLine("Usage: dotnet ExampleUser.dll {verify|complete} gmn_data\n");
                Environment.Exit(1);
            }

            try
            {
                if (args[0].Equals("verify"))
                {
                    bool valid=HealthcareGMN.VerifyCheckCharacters(args[1]);
                    Console.WriteLine("The check characters are " + (valid ? "valid" : "NOT valid"));
                    Environment.Exit(valid ? 0:1);
                }
                else   // complete
                {
                    Console.WriteLine(HealthcareGMN.AddCheckCharacters(args[1]));
                    Environment.Exit(0);
                }
            }
            catch (GS1Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Environment.Exit(1);
            }

        }

    }
}

