
namespace ExperienceSchemas
{
     public class ProcessorFailureDetail
        {
            public ProcessorFailureDetail () {}
            public ProcessorFailureDetail (string message) {
                this.Message = message;
            }
            public string JsonInstanceLocation { get; set; }
            public string JsonKeywordLocation { get; set; }
            public string Message { get; set; }
        }
}