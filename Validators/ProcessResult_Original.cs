using System;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Json.Schema;

namespace ExperienceSchemas
{
    static class ProcessResultsOriginal
    {
        // for some reason every single json value is set as a failure if 1 fails
        // maybe there is a config to avoid that?
        // ignore the useless data
        private static string IGNORE_MESSAGE = "All values fail against the false schema";
        public static void Process(ValidationResults results, bool showResultStructure = false)
        {
            if ( results.IsValid )
            {
                // when result is valid the results.Message isn't empty.
                // it was a new line character - perhaps in the future the message will be populated with something?
                DisplaySuccess(results.Message);
            } else {
                DisplayErrors(results, showResultStructure);
            }
        }

        private static void DisplayErrors (ValidationResults results, bool showResultStructure) {

            // process errors and exceptions captured by validation process
            string message = !String.IsNullOrEmpty(results.Message) ? results.Message : "\nNo Process Errors On Main Schema";
            
            if ( results.NestedResults.Count > 0 )
            {
                // iterate over the list of ValidationResults
                // it might be the Message is only for true Exceptions
                bool foundError = false;
                foreach (ValidationResults r in results.NestedResults) {
                    if ( !String.IsNullOrEmpty(r.Message) ) {
                        foundError = true;
                        message = message + "\n" + r.Message;
                    }
                }

                if (!foundError)
                {
                    message = message + "\nNo Process Errors In Nested Results";
                }

                message += "\n\nSchema Errors:\n";
            }

            // validation errors capture by schema validator
            results.ToDetailed();
            
            var serializerOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};

            
            JsonDocument jdoc = JsonDocument.Parse(JsonSerializer.Serialize(results, serializerOptions));
            JsonElement elem = jdoc.RootElement;

            // "errors": [
            //      { 
            //      "error": "string value",
            //      "errors": [{...turtles...}] 
            //      }
            //  ]
            JsonElementSearchResult topLevelErrors = getElement(elem, "errors");
            message = topLevelErrors.HasKeyword ? message + getErrorMessages(topLevelErrors.Element) +  "\n\nEnd Schema Errors" : message;
            
            if (showResultStructure) {
                Console.WriteLine(elem);
            }
            
            DisplayMessageTemplate(false);
            Console.WriteLine(message);
            
        }

        private static string getErrorMessages (JsonElement elem, string messages="")
        {
            var message = "";

            foreach (JsonElement node in elem.EnumerateArray())
            {
                // look for the messages
                // look for the keyword and location to repair this data
                JsonElementSearchResult elementResult = getElement(node, "error");
                if (elementResult.HasKeyword)
                {
                    string val = elementResult.Element.GetString();
                    if (!String.IsNullOrEmpty(val) && !String.Equals(IGNORE_MESSAGE, val) ) {
                        // these look like they can be used to point to the data that needs fixing
                        // could probably use this to get the actual Entity and fix the data
                        // could definitely fix the JSON
                        JsonElementSearchResult fixableLocation = getElement(node, "keywordLocation");
                        JsonElementSearchResult fixableProperty = getElement(node, "instanceLocation");

                        message = fixableLocation.HasKeyword ? message + "\n" + fixableLocation.Element.GetString() : message;
                        message = fixableProperty.HasKeyword ? message + "\n" + fixableProperty.Element.GetString() : message;
                        message += "\n" + val;
                    }


                }

                // get errors element and process json nodes
                elementResult = getElement(node, "errors");
                if (elementResult.HasKeyword)
                {
                    message += getErrorMessages (elementResult.Element, messages);
                }
            }

            return messages += message;
        }

        public static JsonElementSearchResult getElement (JsonElement elem, string keyword="") {
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

        private static void DisplayMessageTemplate(bool IsValid)
        {
            Console.Write("isValid: ");
            Console.Write(IsValid);
            Console.WriteLine("");
            Console.Write("Result Messages: ");
        }
        private static void DisplaySuccess (string messages) {
            DisplayMessageTemplate(true);
            string message = "Valid JSON";
            Console.WriteLine(message);
        }
    }
}