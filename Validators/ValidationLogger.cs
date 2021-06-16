using System;
using Json.Schema;

namespace ExperienceSchemas
{


 class CarouselLogger : ILog
    {
        public void Write (Func<string> message, int indent)
        {
            Console.WriteLine(message());
        }
    } 

}