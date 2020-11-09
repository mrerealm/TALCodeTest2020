# TALCodeTest2020

Requirements: Calculation of monthly premiums based on Death Cover amount, Occupation Rating Factor, and Age. Formula provided: Death Premium = (Death Cover amount * Occupation Rating Factor * Age) /1000 * 12 All fields required

tech stack: .Net core 3.1, Web API, Angular 8 UI, patterns: SOA (Onion Layer), DI, SOLID Basic solution from VisualStudio 2019 Angular/.Netcore boilplate.

requirements: nodejs is running; angular cli installed; make sure run "ng serve -o" in an Angular project

how to run: open solution in Visual Studio and on default browser.

considerations: Used Reactive forms for validation UI uses bootstrap but not mobile layout; No validation of date of birth; Age is calculated from DOB; Occupation + Ratings provided by backend API; No auto-formatting date or currency
