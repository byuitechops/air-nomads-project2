class GenerateCSV : IReport
{
    public string ReportData { get; set; }
    public string Destination { get; set; }
    public string Format { get; set; }

    public bool GenerateReport(string ReportData)
    {
        throw new System.NotImplementedException();
    }
}

class GenerateHTML : IReport
{
    public string ReportData { get; set; }
    public string Destination { get; set; }
    public string Format { get; set; }

    public bool GenerateReport(string ReportData)
    {
        throw new System.NotImplementedException();
    }
}

class GenerateJSON : IReport
{
    public string ReportData { get; set; }
    public string Destination { get; set; }
    public string Format { get; set; }

    public bool GenerateReport(string ReportData)
    {
        return false;
    }
}

interface IReport
{
    string ReportData { get; set; }
    string Destination { get; set; }
    string Format { get; set; }

    bool GenerateReport(string ReportData);
}

