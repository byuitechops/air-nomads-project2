using System;
using System.Collections.Generic;

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
            System.Console.WriteLine("Course API Endpoint:");
            string apiKey = Console.ReadLine();

            System.Console.WriteLine("Course ID:");
            string courseId = Console.ReadLine();

            System.Console.WriteLine("Report Format (CSV, HTML, JSON):");
            string outFormat = Console.ReadLine();

            System.Console.WriteLine("Destination Path:");
            string destination = Console.ReadLine();

            System.Console.WriteLine("Would you like to add another course? (Y/N)");
            string keepGoing = Console.ReadLine();
            if (keepGoing.ToUpper() == "N")
            {
                run = false;
            }
            else
            {
                PromptList.Add(new Prompt(apiKey, courseId, outFormat, destination));
            }
        }
        return PromptList;
    }
}
// this should be public because a program will be accessing the variable of each prompt
public class Prompt
{
    private string ApiKey;
    private string CourseId;
    private string OutFormat;
    private string Destination;

    public Prompt(string ApiKey, string CourseId, string OutFormat, string Destination)
    {
        this.ApiKey = ApiKey;
        this.CourseId = CourseId;
        this.OutFormat = OutFormat;
        this.Destination = Destination;

    }
}
