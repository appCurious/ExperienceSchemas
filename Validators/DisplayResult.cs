using System;
using System.Text.Json;
using System.Text.Encodings.Web;
using Json.Schema;

namespace ExperienceSchemas
{
    static class DisplayResults 
    {
        private static string IGNORE_MESSAGE = "All values fail against the false schema";
        public static void ProcessResults(ValidationResults results)
        {
            
            // this.results = results;

            Console.Write("isValid: ");
            Console.Write(results.IsValid);
            Console.WriteLine("");
            Console.Write("Result Messages: ");


            if ( results.IsValid && !String.IsNullOrEmpty(results.Message) )
            {
                DisplaySuccess();
            } else {
                DisplayErrors(results);
            }
        }

        private static void DisplayErrors (ValidationResults results) {

            // process errors and exceptions captured by validation process
            string message = !String.IsNullOrEmpty(results.Message) ? results.Message : "No Process Errors On Main Schema";
            
            if ( results.NestedResults.Count > 0 )
            {
                // iterate over the list of ValidationResults
                // it might be the Message is only for true Exceptions
                foreach (ValidationResults r in results.NestedResults) {
                    message = !String.IsNullOrEmpty(r.Message) ? message + "\n" + r.Message : message;
                }
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
            JsonElement topLevelErrors = elem.GetProperty("errors");
            
            // Console.WriteLine(topLevelErrors);
            Console.WriteLine(elem);
            Console.WriteLine("more to see ");
            Console.WriteLine(getErrorMessages(topLevelErrors, ""));

            
        }

        private static string getErrorMessages (JsonElement elem, string messages)
        {
            var message = "";

            foreach (JsonElement node in elem.EnumerateArray())
            {
                JsonElement err = node.GetProperty("error");
                string val = err.GetString();
                message = !String.IsNullOrEmpty(val) && !String.Equals(IGNORE_MESSAGE, val) ? message + "\n" + val : message;
            }

            return message;
        }
        private static void DisplaySuccess () {
            Console.WriteLine("Valid JSON");
        }
    }
}