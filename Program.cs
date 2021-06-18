using System;

namespace ExperienceSchemas
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showResultStructure = false;
            // args[0] displays all the data structure for the results
            // TODO format console logs to be serialized JSON
            if (args.Length > 0)
            {
                // showResultStructure = !String.IsNullOrEmpty(args[0]) ? true : false;
                showResultStructure = !String.Equals(args[0].ToUpper(), "FALSE") ? true : false;
            }

            // args[1] is the fully qualified class name for the validator to use
            // args[2] is the serialized json to validate
            // run a json validation check
            if (args.Length >= 3)
            {
                string validationResult = ValidatorProcessor.RunProcessor(showResultStructure, args[1], args[2]);
                Console.WriteLine(validationResult);
            } else {
                // run the examples
                // the string validation is an example of a valid json structure
                // it is validated against a real schema
                // it produces a valid result
                Console.WriteLine("*** String Validation ***\n");
                IExampleValidator stringValidator = new ExampleStringData();
                stringValidator.ProcessResults(stringValidator.RunExample(), showResultStructure);

                // the entity validation fails the initial validation test
                // this is intentional in order to identify problems in the json
                // and allow the program to fix the issues
                // - now that is a challenge
                // - so far my results indicate there is a problem
                // - but it is identified using a human readable string
                Console.WriteLine("\n*** Entity Validation ***");
                IExampleValidator entityValidator = new ExampleEntityData();
                entityValidator.ProcessResults(entityValidator.RunExample(), showResultStructure);

                Console.WriteLine("\n*** Entity LookUp ***");
                IJsonValidator validator = ValidatorProcessor.GetValidator("ExperienceSchemas.CarouselValidator");
                Type t = validator.GetType();
                Console.WriteLine(t.FullName);
            }
        }
    }
}
