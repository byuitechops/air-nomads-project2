using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReportGeneratorFunctions;
using ConsoleReport;
using AirNomadPrompter;
using AirNomadHttpGrabers;
using AirNomadReportCompile;
using System.IO;
using CsvHelper;

namespace AirNomadReportGenerators
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
                    return new GenerateHTML(destination, "./boilerplate.html");
                case "csv":
                    return new GenerateCSV(destination);
                default:
                    System.Console.WriteLine("YOU ARE UNWORTHY! Loser!!");
                    ConsoleRep.Log(new string[] { $"Warning! We do not have a generator for the type \"{type.ToLower().Trim()}\"!!!", "We will just give you a json instead!" }, ConsoleColor.Yellow, ConsoleColor.DarkBlue);
                    break;
            }

            return new GenerateJSON(destination);
        }
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("We made it this far!");
            await GenerateReports( getPromptsFromType(args));

        }

        public static List<Prompt> getPromptsFromType(string[] args)
        {
            if(args.Length == 0)
                return GenerateReportsFromPrompt();

            switch (args[0])
            {
                case "csv":
                case "c":
                    return  GenerateReportsFromCsv(args[1]);
                case "prompt":
                case "p":
                    return  GenerateReportsFromPrompt();
                default:
                    return  GenerateReportsFromPrompt();
            }
        }
        static List<Prompt> GenerateReportsFromPrompt()
        {
            List<Prompt> prompts = new List<Prompt>();
            try
            {

                /*Gets all necessary input and stores it into a list of prompts */
                prompts = Prompter.PromptUser();
            }
            catch (Exception e)
            {
                ConsoleRep.Log(new string[] { "There was an error collecting the prompt data!", "Error:", e.Message, "Seeing that you have given us absolutely nothing to work with,", "I will just retire to my room until you actually give me something useful!" }, ConsoleColor.Red);

                throw;
            }

            return prompts;
        }

        /* 
        * This returns a list of String arrays from the given csv file
        */
        public static List<string[]> readCsvFromPath(string path)
        {
            var csvLines = new List<string[]>() { };
            using (var reader = new StreamReader(path))
            using (var parser = new CsvParser(reader))
            {
                var hasNext = true;
                while (true)
                {
                    var records = parser.Read();
                    hasNext = (records != null);
                    if (!hasNext) break;
                    csvLines.Add(records);

                }
            }
            return csvLines;
        }

        static List<Prompt> GenerateReportsFromCsv(string filePath)
        {
            List<Prompt> prompts = new List<Prompt>();
            try
            {

                /*Read PROMPTS FROM CSV */
                List<string[]> promptData = readCsvFromPath(filePath);
                //courseid, filetype, destination 
                for (int i = 1; i < promptData.Count; i++)
                {
                    var prompt = new Prompt(promptData[i][0].Trim(), promptData[i][1].Trim(), promptData[i][2].Trim());
                    prompts.Add(prompt);
                }
            }
            catch (Exception e)
            {
                ConsoleRep.Log(new string[] { "There was an error collecting the prompt data!", "Error:", e.Message, "Seeing that you have given us absolutely nothing to work with,", "I will just retire to my room until you actually give me something useful!" }, ConsoleColor.Red);

                throw;
            }
            return prompts;
        }
        static async Task GenerateReports(List<Prompt> prompts)
        {
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
                try
                {
                    success = await compiler.CompileReport();
                }
                catch (Exception e)
                {
                    // Display all errors in an awesome fashion.
                    ConsoleRep.Log(new string[] { "WE GOT AN ERROR BOSS!", e.Message, "Course: " + prompt.CourseId, prompt.OutFormat + " " + prompt.Destination }, ConsoleColor.Red);
                    success = false;
                }
                SuccessReports.Add(new ReportItem(prompt.OutFormat + " " + prompt.Destination + "    =====   " + (success ? "Successful" : "Error!"), success ? ConsoleColor.Green : ConsoleColor.Red));
            }

            ConsoleRep.Log(SuccessReports);
        }
    }
}