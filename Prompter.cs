using System.Collections.Generic;

class Prompter
{
    private string ApiToken;
    private List<Prompt> PromptList;

    public List<Prompt> prompt()
    {
        return PromptList;
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