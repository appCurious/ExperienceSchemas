using System;
using System.Text.Json;
using Json.Schema;
// using Newtonsoft.Json;

namespace ExperienceSchemas
{
    class ExampleEntityData : IExampleValidator
    {
        // TODO pass in a validator and data so that any validator can run
        public ValidationResults RunExample ()
        {
            // use an entity
            ExampleWidget widget = ExampleWidget.getExampleWidget();

            // serialize to get json properties
            JsonSerializerOptions serialOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            string exampleWidgetData = JsonSerializer.Serialize(widget, serialOptions);
            
            JsonValidator exampleValidator = new ExampleWidgetValidator();
        
            // proof of concept utilize serialized entity string
            JsonDocument jdoc = JsonDocument.Parse(exampleWidgetData);

            Console.WriteLine("Prepare to Validate String Data");
            ValidationResults results = exampleValidator.validateData(jdoc.RootElement);

            return results;
            
        }

        public void ProcessResults (ValidationResults results, bool showResultStructure = false) {
            ExperienceSchemas.ProcessResults.Process(results, showResultStructure);
        }
    }
}
