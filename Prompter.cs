using System;
using System.Collections.Generic;
using System.IO;
using ConsoleReport;

namespace air_nomades_projectSquared
{
    public static class Prompter
    {
        private static bool IsValidCourseID(string id)
        {

            if (id.Trim() == "")
                return false;
            int i = 0;
            if (!int.TryParse(id, out i))
                return false;
            return true;
        }
        private static bool IsValidFileType(string type)
        {

            if (type.Trim() == "")
                return false;
            switch (type.ToLower().Trim())
            {
                case "json":
                    return true;
                case "html":
                    return true;
                case "csv":
                    return true;
                default:
                    return false;
            }
        }
        private static bool IsValidDestination(string path)
        {
            if (path.Trim() == "")
            {
                return false;
            }
            else if (!Directory.Exists(path))
            {
                return false;
            }
            return true;
        }
        private static bool IsValidContinueCondition(string response)
        {
            if (response.Trim() == "")
            {
                return false;
            }
            else if (response.Trim().ToUpper() != "N" && response.Trim().ToUpper() != "Y" )
            {
                return false;
            }
            return true;
        }

        private delegate bool Validator(string s);
        private static string getValidData(string prompt, Validator validator, string exresonse = "response")
        {
            var response = "";
            var times = 0;
            do
            {
                times++;
                if (times > 10)
                {
                    ConsoleRep.Log(new string[] { "You obviously have no idea what you are doing! Come back with someone who actually knows how to use this tool" }, ConsoleColor.DarkMagenta, ConsoleColor.Gray);
                    for (var i = 0; i < 3; i++)
                    {
                        Console.Beep();
                    }
                    throw new Exception("YOU ARE INCOMPETENT!");
                }
                else if (times < 5)
                    System.Console.WriteLine((times > 1) ? $"That {exresonse} was not legit man! Try again! " : prompt);
                else
                    ConsoleRep.Log(new string[] { "YOU ARE REALLY TRYING MY PATIENCE NOW!", $"That {exresonse} was not legit man! Try again! ", prompt }, ConsoleColor.DarkMagenta, ConsoleColor.Gray);
                response = Console.ReadLine();
            }
            while (!validator(response));

            if (times >= 2)
                ConsoleRep.Log("THANK YOU INCOMPETENT BABOON!", ConsoleColor.DarkRed);
            return response;
        }
        public static List<Prompt> PromptUser()
        {
            var PromptList = new List<Prompt>();

            var run = true;

            
            while (run)
            {

                string courseId = getValidData("Course ID:", IsValidCourseID, "ID");
                string outFormat = getValidData("Report Format (CSV, HTML, JSON):", IsValidFileType, "Output Format");
                string destination = getValidData("Destination Path:", IsValidDestination, "Destination Path");

                PromptList.Add(new Prompt(courseId, outFormat, destination));

                string keepGoing = getValidData("Would you like to add another course? (Y/N)", IsValidContinueCondition);
                
                if (keepGoing.ToUpper() == "N")
                {
                    run = false;
                }
            }
            return PromptList;
        }
    }
}
// this should be public because a program will be accessing the variable of each prompt
public class Prompt
{
    public string ApiKey { get; set; }
    public string CourseId { get; set; }
    public string OutFormat { get; set; }
    public string Destination { get; set; }

    public Prompt(string CourseId, string OutFormat, string Destination)
    {
        this.CourseId = CourseId;
        this.OutFormat = OutFormat;
        this.Destination = Destination;

    }

}