using System.Text.Json;
using Json.Schema;

namespace ExperienceSchemas
{
    public interface IJsonValidator
    {
        public ValidationResults validateData (JsonElement jsonData);
    }
}