# Experience Schemas
Working to validate JSON data against known JSON schemas.

## JSON Schema Target
We will target JSON Schema Drafts >= 7.

## Args

```
args[0] true | false controls displaying the result structure in the console
args[1] true | false controls console logs
args[2] fully qualified Validator to run - factory will instantiate
args[3] serialized json to validate
```
## Run Test / Demo
Must only use the first 2 args in order for the Examples to run
Example data, schemas and widget are set up to run a successful test and a failed test

```
dotnet run false true
```
Display the json structure for the failed result - a value other than false in the first argument will work
```
dotnet run true true
```
## Run Actual Validator
Must have all 4 args entered in order for a real validator to run
```
dotnet run true false ExperienceSchema.ValidatorNameToRun "{""jsonParam"":""jsonParamValue""}"
```

## Conclusion
Yes it is possible to run a JSON validation test against a JSON Schema.  The results come back with a nice boolean indication of success / failure   IsValid.   Thre is an option to retrieve the the JSON path and Keyword for the schema failure.  

However I have not been successful at programatically identifying what the failure is.  Identifying the reason for failure such as value did not match an enum value, or the const value or was null is essential to fixing the data.

It may be more effective to break the schemas down to specifically target an Entity or an Entity's value for the purpose of being able to fix the problem value with a known default.


## Resource Links
* [json schema](https://json-schema.org/)
* [json validators](https://json-schema.org/implementations.html#validator-dotnet)
* [json serialization](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0)
* [json document](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.jsondocument?view=net-5.0)
* [$ref schemas](https://gregsdennis.github.io/json-everything/usage/schema-references.html?q=ref)

