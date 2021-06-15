using System.Text.Json;
using Json.Schema;

namespace ExperienceSchemas
{
    class CarouselValidator : JsonValidator
    {
        public ValidationResults validateData (JsonElement jsonData) 
        {          
            
            JsonSchema schema = JsonSchema.FromFile("./Validators/Experience/Widgets/carousel.schema.json");

            return schema.Validate(jsonData);
        }
    }
}

