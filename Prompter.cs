using System;
using System.Collections.Generic;

class Prompter
{
    private string ApiToken;
    private List<Prompt> PromptList;

    public List<Prompt> PromptUser()
    {
           var run = true;

            System.Console.WriteLine("Enter course API keys, followed by courseID (type 'exit' when done):");
            while (run)
            {
                string apiKey = Console.ReadLine();
                string courseId = Console.ReadLine();
                string outFormat = Console.ReadLine();
                if (apiKey == "exit")
                {
                    run = false;
                }
                else
                {
                    PromptList.Add(new Prompt(apiKey, courseId, outFormat));
                }
            }
        return this.PromptList;
    }
}

internal class Prompt
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