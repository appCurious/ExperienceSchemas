using System;

namespace ExperienceSchemas
{
    class Program
    {
        static void Main(string[] args)
        {
            ValidationProcessorOptions processorOptions = new ValidationProcessorOptions();

            // args[0] displays all the data structure for the results
            // args[1] outputs results to console like a unit test
            // TODO format console logs to be serialized JSON
            if (args.Length == 2)
            {
                processorOptions.ShowResultStructure = !String.Equals(args[0].ToUpper(), "FALSE") ? true : false;
                processorOptions.LogToConsole =!String.Equals(args[1].ToUpper(), "FALSE") ? true : false;
            }

            // args[2] is the fully qualified class name for the validator to use
            // args[3] is the serialized json to validate
            // run a json validation check
            if (args.Length >= 4)
            {
                processorOptions.ShowResultStructure = !String.Equals(args[0].ToUpper(), "FALSE") ? true : false;
                processorOptions.LogToConsole =!String.Equals(args[1].ToUpper(), "FALSE") ? true : false;

                processorOptions.QualifiedValidatorClass = args[2];
                processorOptions.DotnetSerializedJsonString = args[3];
                string validationResult = ValidationProcessor.RunProcessor(processorOptions);
                
                // provide the output to the calling program using the console
                Console.WriteLine(validationResult);
            } else {
                // run the examples
                // the string validation is an example of a valid json structure
                // it is validated against a real schema
                // it produces a valid result
                Console.WriteLine("*** String Validation ***\n");
                IExampleValidator stringValidator = new ExampleStringData();
                stringValidator.ProcessResults(stringValidator.RunExample(processorOptions), processorOptions);

                // the entity validation fails the initial validation test
                // this is intentional in order to identify problems in the json
                // and allow the program to fix the issues
                // - now that is a challenge
                // - so far my results indicate there is a problem
                // - but it is identified using a human readable string
                Console.WriteLine("\n*** Entity Validation ***");
                IExampleValidator entityValidator = new ExampleEntityData();
                entityValidator.ProcessResults(entityValidator.RunExample(processorOptions), processorOptions);

                Console.WriteLine("\n*** Entity LookUp ***");
                IJsonValidator validator = ValidationProcessor.GetValidator("ExperienceSchemas.CarouselValidator");
                Type t = validator.GetType();
                Console.WriteLine(t.FullName);
            }
        }
    }
}
