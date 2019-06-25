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
            List<Prompt> prompts = Prompter.PromptUser();
            GET request = new GET();
            foreach (Prompt p in prompts)
            {
                //send each Prompt object so to a method inside get or post so the request will be made
                await request.MakeGetRequest(p);
            }
            //2. Receive report
        }
    }
}
