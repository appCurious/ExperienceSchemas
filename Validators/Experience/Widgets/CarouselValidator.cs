using System.Text.Json;
using System;
using Json.Schema;

namespace ExperienceSchemas
{
    class CarouselValidator : JsonValidator
    {
        public ValidationResults validateData (JsonElement jsonData) Throws 
        {          
            try {
                JsonSchema schema = JsonSchema.FromFile("./Validators/Experience/Widgets/carousel.schema.json");

                return schema.Validate(jsonData);

            } catch (Exception e) {
                throw e;
            }
        }
    }
}

