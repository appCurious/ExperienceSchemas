using Json.Schema;
namespace ExperienceSchemas
{
    public interface IExampleValidator
    {
        public ValidationResults RunExample ();
        public void ProcessResults (ValidationResults results, bool showResultStructure);
    }
}