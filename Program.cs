using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReportGeneratorFunctions;
using ConsoleReport;
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

            var SuccessReports = new List<ReportItem>();

            /*Loop through each prompt, set up the http call, calibrate how the compiler should work and send the success reports to the Dictionary we have for keeping track of it */
            foreach (var prompt in prompts)
            {
                http.CourseID = prompt.CourseId;
                compiler.CalibrateCompiler(prompt, grabReportObject(prompt.OutFormat, prompt.Destination), http);
                
                var success = false;
                try {
                    success = await compiler.CompileReport();
                }catch (Exception e){
                    // Display all errors in an awesome fashion.
                    ConsoleRep.Log(new string[]{"WE GOT AN ERROR BOSS!",e.Message,"Course: "+prompt.CourseId, prompt.OutFormat +" "+prompt.Destination}, ConsoleColor.Red);
                    success = false;
                }
                SuccessReports.Add(new ReportItem(prompt.OutFormat +" "+ prompt.Destination + "    =====   " +  (success ? "Successful" : "Error!"), success ? ConsoleColor.Green : ConsoleColor.Red));
            }

            ConsoleRep.Log(SuccessReports);
        }
    }
}