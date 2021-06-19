using System.Collections.Generic;
using Json.Schema;

namespace ExperienceSchemas
{
    public class ProcessorResults
        {
            public List<string> ErrorMessages { get; } = new List<string>();

            public bool IsValid { get; set; }
            public ValidationResults RawResults { get; set; }

            public List<ProcessorFailureDetail> ViolationList { get; } = new List<ProcessorFailureDetail>();
        }
}