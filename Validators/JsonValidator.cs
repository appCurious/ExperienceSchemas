using System.Text.Json;
using Json.Schema;

namespace ExperienceSchemas
{
    interface JsonValidator
    {
        public ValidationResults validateData (JsonElement jsonData);
    }
}