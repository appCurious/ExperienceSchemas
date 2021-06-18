using System;

namespace ExperienceSchemas
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showResultStructure = false;
            if (args.Length > 0)
            {
                showResultStructure = !String.IsNullOrEmpty(args[0]) ? true : false;
            }
            
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
            Console.WriteLine("*** Entity Validation ***");
            IExampleValidator entityValidator = new ExampleEntityData();
            entityValidator.ProcessResults(entityValidator.RunExample(), showResultStructure);
            
        }
    }
}
