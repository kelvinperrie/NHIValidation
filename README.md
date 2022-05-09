# NHIValidation

A c# class to validate New Zealand National Health Index numbers. Checks both old and new formats.

This is based off the Ministry of Health document nhi_validation_routine_July_2020.pdf from https://www.health.govt.nz/our-work/health-identity/national-health-index/information-health-it-vendors-and-developers/nhi-interfaces.

usage is 
```C#
bool valid;
valid = NHIValidation.IsNhiValid(nhi);
valid = NHIValidation.IsNhiValid("PRP1660");  // true - old format AAANNNC
valid = NHIValidation.IsNhiValid("ZGT56KB");  // true - new format AAANNAC
valid = NHIValidation.IsNhiValid("whatever"); // false
```
