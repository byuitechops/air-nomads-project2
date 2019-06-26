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
                    return new GenerateHTML();
                case "csv":
                    return new GenerateCSV();
                default:
                    System.Console.WriteLine("YOU ARE UNWORTHY! Loser!!");
                    break;

            }

            return new GenerateJSON(destination);
        }
        static async Task Main(string[] args)
        {
            //List<Prompt> prompts = Prompter.PromptUser();
            var prompt = new Prompt("59796", "json", "disworked");
            CourseGrabber http = new CourseGrabber(prompt.CourseId);
            DoNothingGrabber nothing = new DoNothingGrabber();
            var format = prompt.OutFormat;
            var dest = prompt.Destination;
             grabReportObject(format, dest);
            var compiler = new ReportCompile(prompt, grabReportObject(format, dest), new GetAccountHandler());
            await compiler.CompileReport();
            //var response = await http.GrabCourseObject();
            //System.Console.WriteLine(JsonConvert.SerializeObject(response));
            // GenerateJSON report = new GenerateJSON();
        }
    }
}
