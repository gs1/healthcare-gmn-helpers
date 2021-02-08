GS1 Healthcare GMN Helper Libraries (deprecated)
================================================

The GS1 Healthcare GMN Helper Libraries is an open source project that contains
a set of official helper libraries written by GS1 for check character pair
generation and verification of a Global Model Number (GMN) when it is used for
Regulated Healthcare medical devices that fall under the EU regulations EU MDR
2017/745 and EU IVDR 2017/746, specifically when a GMN is used as the embodiment
of a Basic UDI-DI.

**Note:** With the GS1 General Specifications Release 21.0 in January 2021, all
uses of the GS1 Global Model Number have now been harmonized. The GMN is
universally a 25-character maximum length identifier that is protected by the
same standard check character pair validation algorithm, irrespective of the
application (whether healthcare or otherwise).

**A new, universal release of the Global Model Number helper libraries has been
published and all users should now migrate to this:**
<https://github.com/gs1/gmn-helpers>

The code in the new release is functionally equivalent to this library in every
way with the exception that the class name has been amended from
`HealthcareGMN` to `GMN` to reflect the now-universal definition of the GMN.

Whereas you might have previously called a library method with:

    bool valid = HealthcareGMN.VerifyCheckCharacters("1987654Ad4X4bL5ttr2310c2K");

With the new library you would call the same method with:

    bool valid = GMN.VerifyCheckCharacters("1987654Ad4X4bL5ttr2310c2K");

Other than renaming any class imports or class-qualified method calls there are
no other changes required for user code when migrating to the new library.


License
-------

Copyright (c) 2019 GS1 AISBL

Licensed under the Apache License, Version 2.0 (the "License"); you may not use
this library except in compliance with the License.

You may obtain a copy of the License at:

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
CONDITIONS OF ANY KIND, either express or implied. See the License for the
specific language governing permissions and limitations under the License.
