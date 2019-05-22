org.gs.HealthcareGMN Java Helper Library
========================================

This contains an official Java helper library provided by GS1 for check
character pair generation and verification of a Global Model Number (GMN) when
it is used for Regulated Healthcare medical devices that fall under the EU
regulations EU MDR 2017/745 and EU IVDR 2017/746, specifically when a GMN is
used as the embodiment of a Basic UDI-DI.

Artifacts
---------

| Asset                      | Purpose                                                              |
| -------------------------- | -------------------------------------------------------------------- |
| HealthcareGMN.jar          | The helper library packaged as a standard JAR file                   |
| docs/index.html            | Documentation describing the library's API                           |
| org/gs1/HealthcareGMN.java | Source code for the utility class that implements the helper library |
| HealthcareGMNTests.java    | Unit tests for the utility class compatible with JUnit 4 or later    |
| ExampleUser.java           | Example code providing a simple application that uses the library    |


Using the helper library
------------------------

The JAR package can be placed in your local or organisational package
repository and configured as a dependancy of your solution, amending your
classpath as necessary.

Alternatively, the source code for the utility class can be vendored in to your
solution's source code.

Finally, you could examine the source code then create your own specialised
implementation.  In this case you should apply the unit tests to your own code
as a means of detecting errors. The unit tests are generalised to make this
easy provided that you use matching method names.


Documentation
-------------

HTML documentation of the API is included in this distribution at
docs/index.html


Building the helper library
---------------------------

From this source directory:

    javac org/gs1/HealthcareGMN.java

If you have modified the class then you should run the tests using JUnit 4:

    javac -cp .:[...]/junit4.jar HealthcareGMNTests.java
    java  -cp .:[...]/junit4.jar org.junit.runner.JUnitCore HealthcareGMNTests


Building and running the example application
--------------------------------------------

From this source directory:

    javac ExampleUser.java
    java  ExampleUser

Or using the JAR file (Windows):

    javac -cp .;HealthcareGMN.jar ExampleUser.java
    java  -cp .;HealthcareGMN.jar ExampleUser

Or using the JAR file (Unix):

    javac -cp .:HealthcareGMN.jar ExampleUser.java
    java  -cp .:HealthcareGMN.jar ExampleUser

When the example application is run without command line arguments it will
display the output of some trivial non-interactive library operations on static
data. It will then enter an interactive mode in which it prompts the user for
an activity to perform and then the data or file to perform the given activity
on.

Additionally, the example application can be called with command line arguments
in which case it will serve as a basic check character pair generation and
verification utility:

    $ java ExampleUser complete 1987654Ad4X4bL5ttr2310c
    1987654Ad4X4bL5ttr2310c2K

    $ java ExampleUser verify 1987654Ad4X4bL5ttr2310c2K
    The check characters are valid

    $ java ExampleUser verify 1987654Ad4X4bL5ttr2310cXX
    The check characters are NOT valid


Recreating the JAR package
--------------------------

From this source directory:

    javac org/gs1/HealthcareGMN.java
    jar -cvf HealthcareGMN.jar org/gs1/*.class


Regenerating the documentation
------------------------------

From this source directory:

    javadoc -d docs org.gs1

