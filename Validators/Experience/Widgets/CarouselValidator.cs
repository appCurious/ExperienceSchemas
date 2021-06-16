using System;
using System.Text.Json;
using Json.Schema;


namespace ExperienceSchemas
{
   
    class CarouselValidator : JsonValidator
    {
        public ValidationResults validateData (JsonElement jsonData) 
        {          
            // options for validation //
            // expect to extract this out and pass it into the validateData function

            ValidationOptions options = new ValidationOptions();
            
            options.OutputFormat = Json.Schema.OutputFormat.Verbose;
            options.LogIndentLevel = 0;

            // main validation schema //
            JsonSchema schema = JsonSchema.FromFile("./Validators/Experience/Widgets/carousel.schema.json");
            

            // dependency validation schemas //
            // image asset
            JsonSchema imageAssetSchema = JsonSchema.FromFile("./Validators/Experience/asset-image.schema.json");
            Uri imageUri = new Uri("https://wherever.com/schemas/ec/asset-image-art-directed.schema.json");
            SchemaRegistry.Global.Register(imageUri, imageAssetSchema);

            // docs indicate Register will take a string but i get a message cannot convert string to Uri
            // https://gregsdennis.github.io/json-everything/usage/schema-references.html?q=ref
            // SchemaRegistry.Global.Register("https://wherever.com/schemas/ec/asset-image-art-directed.schema.json", imageAssetSchema);
            // SchemaRegistry.Global.Register("blah", imageAssetSchema);

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

            // might need this instead of global
            // options.SchemaRegistry.Register()
            ValidationResults results = schema.Validate(jsonData, options);
            
            return schema.Validate(jsonData, options);
        }
    }
}

