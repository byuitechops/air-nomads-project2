using System;
namespace ReportGeneratorFunctions
{
    public class GenerateCSV : IReport
    {
        public string ReportData { get; set; }
        public string Destination { get; set; }
        public string Format { get; set; }

        public bool GenerateReport(string ReportData)
        {
            throw new System.NotImplementedException();
        }
    }

    public class GenerateHTML : IReport
    {
        public string ReportData { get; set; }
        public string Destination { get; set; }
        public string Format { get; set; }

        public bool GenerateReport(string ReportData)
        {
            throw new System.NotImplementedException();
        }
    }

    public class GenerateJSON : IReport
    {
        public string ReportData { get; set; }
        private string _Destination;
        public string Destination
        {
            get{
                return _Destination;
            }
            set{
                if(!value.Contains(".json"))
                    _Destination = value+".json";
                else
                _Destination = value;
            }
        }
        public GenerateJSON(string dest)
        {
            this.Destination = dest;
        
        }

        public bool GenerateReport(string ReportData)
        {


            try
            {
                System.IO.File.WriteAllText(this.Destination, ReportData);
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
        }
    }

    // Should we just make this an abstract public class so that we don't have to repeat variables?
    public interface IReport
    {
        string ReportData { get; set; }
        string Destination { get; set; }

        bool GenerateReport(string ReportData);
    }

}