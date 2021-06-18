using System.Text.Json;

namespace ExperienceSchemas
{
    public class JsonElementSearchResult {
        public bool HasKeyword {get; set;}
        public JsonElement Element {get; set;}

        public string Message { get; set; }
    }
}