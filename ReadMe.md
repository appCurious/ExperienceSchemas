# Experience Schemas
Working to validate Enhanced Content Experiences and Experience Widget data against known JSON schemas.

## JSON Schema Target
We will target JSON Schema Drafts >= 7.

## Test Run
Example data, schemas and widget are set up to run a successful test and a failed test
```
dotnet run
```
Display the json structure for the failed result - a value in the first argument will work
```
dotnet run true
```

## Conclusion
Yes it is possible to run a JSON validation test against a JSON Schema.  The results come back with a nice boolean indication of success / failure   IsValid.   Thre is an option to retrieve the the JSON path and Keyword for the schema failure.  However I have not been successful at programatically identifying what the failure is.  Identifying the reason for failure such as value did not match an enum value, or the const value or was null is essential to fixing the data.


## Resource Links
[json schema](https://json-schema.org/)
[json validators](https://json-schema.org/implementations.html#validator-dotnet)
[json serialization](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0)
[json document](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.jsondocument?view=net-5.0)
[$ref schemas](https://gregsdennis.github.io/json-everything/usage/schema-references.html?q=ref)

