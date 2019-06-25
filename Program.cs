using System;

namespace air_nomades_projectSquared
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Call prompter
            var prompts = Prompter.PromptUser();
            foreach (var prompt in prompts)
            {
                System.Console.WriteLine($"You want to create a {prompt.OutFormat} file from course {prompt.CourseId} and save it to {prompt.Destination}. Is that correct?");
            }

            //2. Receive report
        }
    }
}
