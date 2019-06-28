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


        private string ConvertCourseToCSV(string ReportData)
        {
            var course = JsonConvert.DeserializeObject<Course>(ReportData);
            string csvOutput = "";
            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer))
            {

                // WRITE THE CSV HEADERS
                csv.WriteField("Course_ID");
                csv.WriteField("Module_ID");
                var ModuleItemHeaders = course.Modules[0].Module_Items[0].GetType()
                                   .GetProperties()
                                   .ToList()
                                   .Select((property) =>
                                   {
                                       return property.Name;
                                   });
                foreach (var header in ModuleItemHeaders)
                {
                    csv.WriteField(header);
                }
                csv.NextRecord();
                // WRITE THE CONTENT IN THE CSV
                foreach (var module in course.Modules)
                {

                    foreach (var module_item in module.Module_Items)
                    {
                        // GRAB ALL THE DATA FOR EACH MODULE ITEM AND PUTS IT INTO A LIST
                        var ModuleItemData = module_item.GetType()
                                   .GetProperties()
                                   .ToList()
                                   .Select((property) =>
                                   {
                                       return property.GetValue(module_item);
                                   });
                        // WRITES THE COURSE ID IN THE FIRST COLUMN
                        csv.WriteField(course.id);
                        // WRITES EACH ATTRIBUTE OF THE MODULE ITEM
                        foreach (var item in ModuleItemData)
                        {
                            csv.WriteField(item);
                        }
                        csv.NextRecord();
                    }
                }

                // csv.WriteField(column.Value.ToString());
                // csv.NextRecord();
                writer.Flush();
                // EXPORTS OUR CSV STRING
                csvOutput = (writer.ToString());
            }
            return csvOutput;
        }


        public bool GenerateReport(string ReportData)
        {
            try
            {
                var csvstring = ConvertCourseToCSV(ReportData);
                System.IO.File.WriteAllText(_Destination, csvstring);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        public bool GenerateReport(string ReportData, string[] headers)
        {
            return GenerateReport(ReportData);
        }
    }

    public class GenerateHTML : IReport
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
                if (!value.Contains(".html"))
                    _Destination = value + ".html";
                else
                    _Destination = value;
            }
        }


        public GenerateHTML() { }
        public GenerateHTML(string dest)
        {
            this.Destination = dest;
        }

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
            string HtmlFile = "";
            var course = JsonConvert.DeserializeObject<Course>(ReportData);
            HtmlFile += GetHead();
            HtmlFile += BuildTitle(course);



            return false;
        }

        private string GetHead()
        {

            return "";
        }

        private string BuildTitle(Course course)
        {

            return "";
        }

        private string BuildModule(Module module)
        {

            var moduleHtml = "<div class=\"card\">" +
                "<div class=\"card-header\" id=\"headingOne\"><h2 class=\"mb-0\">" +
                    $"<a class=\"btn btn-primary\" data-toggle=\"collapse\" href=\"#{module.id}\" aria-expanded=\"false\"aria-controls=\"{module.id}\">" +
                        $"<strong>{module.name}</strong></a></h2></div>" +
                $"<div class=\"collapse\" id=\"{module.id}\">";

            foreach (var moduleItem in module.Module_Items)
            {
                moduleHtml += BuildModuleItem(moduleItem);
            }
            moduleHtml += "</div>";
            return moduleHtml;
        }

        private string BuildModuleItem(Module_Item moduleItem)
        {
            var itemHtml = "<a href=\"https://canvas.example.edu/courses/222/modules/items/768\">" +
                "<div class=\"card\">" +
                    "<div class=\"card-body\">" +
                        "<strong>Syllabus</strong>" +
                        "<i class=\"material-icons ";
            if (moduleItem.published)
            {
                itemHtml += "published";
            }
            else
            {
                itemHtml += "unpublished";
            }
            itemHtml += "\">";
            itemHtml += GetModuleItemType(moduleItem.type);
            itemHtml += "</i></div></div></a>";

            return itemHtml;
        }

        private string GetModuleItemType(string moduleItemType)
        {
            if (moduleItemType == "File")
            {
                return "folder";
            }
            else if (moduleItemType == "Discussion")
            {
                return "forum";
            }
            else if (moduleItemType == "Quiz")
            {
                return "alarm";
            }
            else if (moduleItemType == "ExternalTool")
            {
                return "launch";
            }
            else
            {
                return "assignment";
            }
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
                throw e;
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