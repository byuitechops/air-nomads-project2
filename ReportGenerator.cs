using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using CanvasObjects;
using CsvHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ReportGeneratorFunctions
{

    public class GenerateCSV : IReport
    {
        public string ReportData { get; set; }
        private string _Destination;
        public string Destination
        {
            get
            {
                return _Destination;
            }
            set
            {
                if (!value.Contains(".csv"))
                    _Destination = value + ".csv";
                else
                    _Destination = value;
            }
        }

        public GenerateCSV() { }
        public GenerateCSV(string dest)
        {
            this.Destination = dest;
        }

        public string Format { get; set; }


        private string ConvertToCSV(string ReportData)
        {
            var course = JsonConvert.DeserializeObject<Course>(ReportData);
            string csvOutput = "";
            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer))
            {

                foreach(var module in course.Modules)
                    foreach(var module_item in module.Module_Items)
                        System.Console.WriteLine(
                             string.Join("," , 
                        module_item.GetType()
                                   .GetProperties()
                                   .ToList()
                                   .Select((property) => {
                                        return property.GetValue(module_item);
                                   }))); 
                // csv.WriteField(column.Value.ToString());
                // csv.NextRecord();
                writer.Flush();
                csvOutput = (writer.ToString());
            }
            return csvOutput;
        }


        public bool GenerateReport(string ReportData)
        {
            try
            {
                var csvstring = ConvertToCSV(ReportData);
                System.IO.File.WriteAllText(_Destination, csvstring);
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }


        }

        public bool GenerateReport(string ReportData, string[] headers)
        {
            //var csvstring = convertToCSV<Course>(JsonConvert.DeserializeObject<Course>(ReportData), headers);
            return GenerateReport(ReportData);
        }
    }

    public class GenerateHTML : IReport
    {
        public string ReportData { get; set; }
        public string Destination { get; set; }
        public string Format { get; set; }

        public bool GenerateReport(string ReportData, string[] headers)
        {
            /*
             * TODO: Add header filter functionality here!
             */
            return GenerateReport(ReportData);
        }

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
            get
            {
                return _Destination;
            }
            set
            {
                if (!value.Contains(".json"))
                    _Destination = value + ".json";
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
        public bool GenerateReport(string ReportData, string[] headers)
        {
            /*
             * TODO: Add header filter functionality here!
             */
            return GenerateReport(ReportData);
        }
    }

    // Should we just make this an abstract public class so that we don't have to repeat variables?
    public interface IReport
    {
        string ReportData { get; set; }
        string Destination { get; set; }

        bool GenerateReport(string ReportData);
        bool GenerateReport(string ReportData, string[] headers);
    }

}