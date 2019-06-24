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

        System.Console.WriteLine("Enter course API key, followed by courseID (type 'exit' when done):");
        while (run)
        {
            string apiKey = Console.ReadLine();
            string courseId = Console.ReadLine();
            string outFormat = Console.ReadLine();
            string keepGoing = Console.ReadLine();
            System.Console.WriteLine("Would you like to add another course? (Y/N)");
            if (keepGoing == "N")
            {
                run = false;
            }
            else
            {
                PromptList.Add(new Prompt(apiKey, courseId, outFormat));
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

    public Prompt(string ApiKey, string CourseId, string OutFormat)
    {
        this.ApiKey = ApiKey;
        this.CourseId = CourseId;
        this.OutFormat = OutFormat;
    }
}
