using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace air_nomades_projectSquared
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //1. Call prompter
<<<<<<< HEAD
            var prompts = Prompter.PromptUser();
            foreach (var prompt in prompts)
            {
                System.Console.WriteLine($"You want to create a {prompt.OutFormat} file from course {prompt.CourseId} and save it to {prompt.Destination}. Is that correct?");
            }

=======
            List<Prompt> prompts = Prompter.PromptUser();
            GET request = new GET();
            foreach (Prompt p in prompts)
            {
                //send each Prompt object so to a method inside get or post so the request will be made
                await request.MakeGetRequest(p);
            }
>>>>>>> b9257d2be137a92f38b5a4946bfa9e5835bacd66
            //2. Receive report
        }
    }
}
