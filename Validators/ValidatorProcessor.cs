using System;
using System.Text.Json;
using Json.Schema;

namespace ExperienceSchemas
{
    public static class ValidatorProcessor
    {
        public static string RunProcessor (bool showResultStructure, string qualifiedValidatorClass, string dotnetSerializedJsonString)
        {
            ProcessorResults processorResults = new ProcessorResults();
            JsonSerializerOptions serialOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

            try {
                IJsonValidator validator = GetValidator(qualifiedValidatorClass);
                JsonElementSearchResult jsonReady = ConvertStringToJsonElement(dotnetSerializedJsonString);

                if (jsonReady.HasKeyword)
                {
                    ValidationResults results = validator.validateData(jsonReady.Element);
                    Type t = validator.GetType();
                    Console.WriteLine($"created {t.FullName}");

                    processorResults.Results = ProcessResults (results, showResultStructure);
                } else {
                    // json did not find the root element from the json parsing, but it didn't fail parsing either
                    processorResults.ErrorMessage = "JSON parsing did not result in a testable structure";
                }

            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                processorResults.ErrorMessage = e.Message;

                return JsonSerializer.Serialize(processorResults, serialOptions);
            }

            return JsonSerializer.Serialize(processorResults, serialOptions);
        }
        public static  JsonElementSearchResult ConvertStringToJsonElement (string dotnetSerializedJsonString) {
            JsonElementSearchResult results = new JsonElementSearchResult();
            results.HasKeyword = false;

            try
            {
                JsonDocument jdoc = JsonDocument.Parse(dotnetSerializedJsonString);
                results.Element = jdoc.RootElement;
                results.HasKeyword = true;
            } catch {
                // could catch the Exception and find out what's up
                results.Message = "Provided String Failed JSON Parsing";
                return results;
            }
            
            return results;
        }

        // would be super awesome to get a list of Validators by naming convention and populate a list
        //  then select the matching instance by Class, Class Name or Type
        public static IJsonValidator GetValidator (string validatorName)
        {
            IJsonValidator validator;
            try {
                Type t = Type.GetType(validatorName);
                validator = (IJsonValidator)Activator.CreateInstance(t);
            } catch (Exception e){
                Console.WriteLine($"Captured {e.Message}");
                throw new Exception($"Unable to Create Validator from {validatorName}");
            }

            return validator;
        }

        public static string ProcessResults (ValidationResults results, bool showResultStructure) {
            //TODO extract the ProcessResults.cs functionality into here to build an object that can be serialized
            // process message on main result and on nested results
            // process nested errors --> retrieve the error, location and keyword
            // create a list of the errors

            // TODO maybe just rebuild the ProcessResults to return an object that can be serialized

            return JsonSerializer.Serialize(results);
        }
        
        public class ProcessorResults
        {
            public string ErrorMessage { get; set; }
            public string Results { get; set; }
        }
    }

}