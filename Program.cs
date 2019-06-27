using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReportGeneratorFunctions;
namespace air_nomades_projectSquared
{
    class Program
    {
        public static IReport grabReportObject(string type, string destination)
        {

            switch (type.ToLower().Trim())
            {
                case "json":
                    return new GenerateJSON(destination);
                case "html":
                    return new GenerateHTML(destination);
                case "csv":
                    return new GenerateCSV(destination);
                default:
                    System.Console.WriteLine("YOU ARE UNWORTHY! Loser!!");
                    break;
            }

            return new GenerateJSON(destination);
        }
        static async Task Main(string[] args)
        {
            /*Gets all necessary input and stores it into a list of prompts */
            List<Prompt> prompts = Prompter.PromptUser();
            /*We will now initialize some objects that will be used as we go execute the call for each prompt */
            var compiler = new ReportCompile();
            CourseGrabber http = new CourseGrabber();
            
            var SuccessReports = new Dictionary<string, bool>();
            
            /*Loop through each prompt, set up the http call, calibrate how the compiler should work and send the success reports to the Dictionary we have for keeping track of it */
            foreach (var prompt in prompts)
            {
                http.CourseID = prompt.CourseId;
                compiler.CalibrateCompiler(prompt, grabReportObject(prompt.OutFormat, prompt.Destination), http);
                var success = await compiler.CompileReport();
                SuccessReports.Add(prompt.Destination, success);
            }

            foreach (var item in SuccessReports)
            {
                System.Console.WriteLine("*******************");
                System.Console.WriteLine(item.Key + " ====== " + (item.Value ? "Successful" : "Error!"));
                System.Console.WriteLine("*******************");
            }
        }
    }
}
