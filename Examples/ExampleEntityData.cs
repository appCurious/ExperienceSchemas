using System;
using System.Text.Json;
using Json.Schema;
// using Newtonsoft.Json;

namespace ExperienceSchemas
{
    class ExampleEntityData : IExampleValidator
    {
        // TODO pass in a validator and data so that any validator can run
        public ValidationResults RunExample (ValidationProcessorOptions processorOptions)
        {
            // use an entity
            ExampleWidget widget = ExampleWidget.getExampleWidget();

            // serialize to get json properties
            // using the Web defaults will camelCase Properties of the Entity
            JsonSerializerOptions serialOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            string exampleWidgetData = JsonSerializer.Serialize(widget, serialOptions);
            
            // using the processorOptions we could call the real ValidationProcessor
            // this Example file was the original proof of concept for the ValidatorProcessor
            IJsonValidator exampleValidator = new ExampleWidgetValidator();
        
            // proof of concept utilize serialized entity string
            JsonDocument jdoc = JsonDocument.Parse(exampleWidgetData);

            Console.WriteLine("Prepare to Validate String Data");
            ValidationResults results = exampleValidator.validateData(jdoc.RootElement);

            return results;
            
        }

        public void ProcessResults (ValidationResults results, ValidationProcessorOptions processorOptions) {
            ExperienceSchemas.ValidationResultProcessor.Process(results, processorOptions);
        }
    }
}
