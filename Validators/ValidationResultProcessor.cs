using System;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Json.Schema;

namespace ExperienceSchemas
{
    static class ValidationResultProcessor
    {
        // for some reason every single json value is set as a failure if 1 fails
        // maybe there is a config to avoid that?
        // ignore the useless data
        private static string IGNORE_MESSAGE = "All values fail against the false schema";
        public static ProcessorResults Process(ValidationResults results, ValidationProcessorOptions processorOptions)
        {
            ProcessorResults processorResults = new ProcessorResults();
            processorResults.IsValid = results.IsValid;
            processorResults.RawResults = results;

            if ( results.IsValid )
            {
                // when result is valid the results.Message isn't empty.
                // it was a new line character - perhaps in the future the message will be populated with something?
                if (processorOptions.LogToConsole)
                {   
                    DisplayConsoleTemplate(true);
                    Console.WriteLine("Valid JSON");
                }

            } else {
                processorResults = ProcessErrors(results, processorResults);
                if (processorOptions.LogToConsole)
                {
                    DisplayConsoleTemplate(false);
                    if (processorOptions.ShowResultStructure) {                        
                        Console.WriteLine(processorResults.RawResults);
                    }
                    foreach (string m in processorResults.ErrorMessages)
                    {
                        Console.WriteLine(m);
                    }
                    foreach (ProcessorFailureDetail detail in processorResults.ViolationList)
                    {
                        Console.WriteLine(detail.Message);
                        if (!String.IsNullOrEmpty(detail.JsonInstanceLocation))
                        {
                            Console.WriteLine(detail.JsonInstanceLocation);
                        }
                        if (!String.IsNullOrEmpty(detail.JsonKeywordLocation))
                        {
                            Console.WriteLine(detail.JsonKeywordLocation);
                        }
                    }
                }
            }

            return processorResults;
        }

        private static ProcessorResults ProcessErrors (ValidationResults results, ProcessorResults processorResults) {
            
            // process errors and exceptions captured by validation process
            processorResults.ErrorMessages.Add(!String.IsNullOrEmpty(results.Message) ? results.Message : "No Process Errors On Main Schema");

            if ( results.NestedResults.Count > 0 )
            {
                // iterate over the list of ValidationResults
                // it might be the Message is only for true Exceptions
                bool foundError = false;
                foreach (ValidationResults r in results.NestedResults) {
                    if ( !String.IsNullOrEmpty(r.Message) ) {
                        foundError = true;
                        processorResults.ErrorMessages.Add(r.Message);
                    }
                }

                if (!foundError)
                {
                    processorResults.ErrorMessages.Add("No Process Errors In Nested Results");
                }
            }

            var serializerOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};

            // validation errors capture by schema validator
            results.ToDetailed();
            JsonDocument jdoc = JsonDocument.Parse(JsonSerializer.Serialize(results, serializerOptions));
            JsonElement elem = jdoc.RootElement;

            // "errors": [
            //      { 
            //      "error": "string value",
            //      "errors": [{...turtles...}] 
            //      }
            //  ]
            JsonElementSearchResult topLevelErrors = GetElement(elem, "errors");
            if (topLevelErrors.HasKeyword) {
                processorResults.ViolationList.Add(new ProcessorFailureDetail("Schema Validation Errors:"));
                processorResults = PopulateFailureDetails(topLevelErrors.Element, processorResults);
                processorResults.ViolationList.Add(new ProcessorFailureDetail("End Schema Validation Errors"));
            }
            

            return processorResults;
            
        }

        private static ProcessorResults PopulateFailureDetails (JsonElement elem, ProcessorResults processorResults)
        {
            foreach (JsonElement node in elem.EnumerateArray())
            {
                // look for the messages
                // look for the keyword and location to repair this data
                JsonElementSearchResult elementResult = GetElement(node, "error");
                if (elementResult.HasKeyword)
                {
                    string val = elementResult.Element.GetString();
                    if (!String.IsNullOrEmpty(val) && !String.Equals(IGNORE_MESSAGE, val) ) {
                        // these look like they can be used to point to the data that needs fixing
                        // could probably use this to get the actual Entity and fix the data
                        // could definitely fix the JSON

                        ProcessorFailureDetail detail = new ProcessorFailureDetail();
                        detail.Message = val;


                        JsonElementSearchResult fixableLocation = GetElement(node, "keywordLocation");
                        JsonElementSearchResult fixableProperty = GetElement(node, "instanceLocation");

                        if (fixableLocation.HasKeyword) {
                            detail.JsonInstanceLocation = fixableLocation.Element.GetString();
                        }

                        if (fixableProperty.HasKeyword) {
                            detail.JsonKeywordLocation = fixableProperty.Element.GetString();
                        }

                        processorResults.ViolationList.Add(detail);
                    }
                }

                // get errors element and process json nodes
                elementResult = GetElement(node, "errors");
                if (elementResult.HasKeyword)
                {
                   processorResults = PopulateFailureDetails(elementResult.Element, processorResults);
                }
            }

            return processorResults;
        }

        public static JsonElementSearchResult GetElement (JsonElement elem, string keyword="") {
            JsonElementSearchResult searchResult = new JsonElementSearchResult();
            searchResult.HasKeyword = false;

            try {

                JsonElement foundElement = elem.GetProperty(keyword);
                searchResult.HasKeyword = true;
                searchResult.Element = foundElement;

            } catch (KeyNotFoundException e) {
                // nothing to do - there are no remaining errors to traverse
                searchResult.Message = e.Message;
                return searchResult;
            }

            return searchResult;
        }

        private static void DisplayConsoleTemplate(bool IsValid)
        {
            Console.Write("isValid: ");
            Console.Write(IsValid);
            Console.WriteLine("");
            Console.Write("Result Messages: ");
        }
    }
}