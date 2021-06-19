using Json.Schema;
namespace ExperienceSchemas
{
    public interface IExampleValidator
    {
        public ValidationResults RunExample (ValidationProcessorOptions processorOptions);
        public void ProcessResults (ValidationResults results, ValidationProcessorOptions processorOptions);
    }
}