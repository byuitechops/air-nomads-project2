using System;
using System.Collections.Generic;
namespace air_nomades_projectSquared
{
    public static class Prompter
    {
        //private string ApiToken;
        //private List<Prompt> PromptList;

        public static List<Prompt> PromptUser()
        {
            var PromptList = new List<Prompt>();

            var run = true;

            //System.Console.WriteLine("Enter course API key and courseID (type 'exit' when done):");
            while (run)
            {
                // // System.Console.WriteLine("Course API Endpoint:");
                // string apiKey =  //Console.ReadLine();

                System.Console.WriteLine("Course ID:");
                string courseId = Console.ReadLine();

                System.Console.WriteLine("Report Format (CSV, HTML, JSON):");
                string outFormat = Console.ReadLine();

                System.Console.WriteLine("Destination Path:");
                string destination = Console.ReadLine();

                PromptList.Add(new Prompt(courseId, outFormat, destination));
                System.Console.WriteLine("Would you like to add another course? (Y/N)");
                string keepGoing = Console.ReadLine();
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