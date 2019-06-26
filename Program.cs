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
            List<Prompt> prompts = Prompter.PromptUser();
            //var prompt = new Prompt("59796", "json", "disworked");
            var compiler = new ReportCompile();
            CourseGrabber http = new CourseGrabber();
            var SuccessReports = new Dictionary<string, bool>();
            System.Console.WriteLine("Prompts: "+prompts.Count);
            foreach (var prompt in prompts)
            {
                http.CourseID = prompt.CourseId;
                compiler.CalibrateCompiler(prompt, grabReportObject(prompt.OutFormat, prompt.Destination), http);
                SuccessReports.Add(prompt.Destination, await compiler.CompileReport());
            }

            foreach(var item in SuccessReports){
                System.Console.WriteLine(item.Key +" ====== " + (item.Value ?  "Successful" : "Error!" ));
            }

            //var response = await http.GrabCourseObject();
            //System.Console.WriteLine(JsonConvert.SerializeObject(response));
            // GenerateJSON report = new GenerateJSON();
        }
    }
}
