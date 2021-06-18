using System.Text.Json;
using Json.Schema;

namespace ExperienceSchemas
{
    interface IJsonValidator
    {
        public ValidationResults validateData (JsonElement jsonData);
    }
}