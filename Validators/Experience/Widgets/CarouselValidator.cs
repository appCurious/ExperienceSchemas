using System;
using System.Text.Json;
using Json.Schema;


namespace ExperienceSchemas
{
   
    class CarouselValidator : IJsonValidator
    {
        public ValidationResults validateData (JsonElement jsonData) 
        {          
            // options for validation //
            // expect to extract this out and pass it into the validateData function
            ValidationOptions options = new ValidationOptions();
            
            // verbose makes the schema failure location and keyword available
            options.OutputFormat = Json.Schema.OutputFormat.Verbose;

            // main validation schema //
            JsonSchema schema = JsonSchema.FromFile("./Validators/Experience/Widgets/carousel.schema.json");

            // dependency validation schemas //
            // image asset
            JsonSchema imageAssetSchema = JsonSchema.FromFile("./Validators/Experience/asset-image.schema.json");
            Uri imageUri = new Uri("https://wherever.com/schemas/ec/asset-image.schema.json");
            SchemaRegistry.Global.Register(imageUri, imageAssetSchema);

            // art directed image asset
            JsonSchema imageArtDirectedSchema = JsonSchema.FromFile("./Validators/Experience/asset-image-art-directed.schema.json");
            Uri artDirectedUri = new Uri("https://wherever.com/schemas/ec/asset-image-art-directed.schema.json");
            SchemaRegistry.Global.Register(artDirectedUri, imageArtDirectedSchema);

            // video asset
            JsonSchema videoAssetSchema = JsonSchema.FromFile("./Validators/Experience/asset-video.schema.json");
            Uri videoUri = new Uri("https://wherever.com/schemas/ec/asset-video.schema.json");
            SchemaRegistry.Global.Register(videoUri, videoAssetSchema);

            // css text color pattern
            JsonSchema cssColorSchema = JsonSchema.FromFile("./Validators/Experience/rgb-color.schema.json");
            Uri colorUri = new Uri("https://wherever.com/schemas/ec/rgb-color.schema.json");
            SchemaRegistry.Global.Register(colorUri, cssColorSchema);

            ValidationResults results = schema.Validate(jsonData, options);
            
            return schema.Validate(jsonData, options);
        }
    }
}

