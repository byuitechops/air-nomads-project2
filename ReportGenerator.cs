using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CanvasObjects;
using CsvHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ReportGeneratorFunctions
{


    /****************************************************************************
    *                                                                           *
    *                                                                           *
    ****************************************************************************/
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
                var directoryPath = value;
                System.Console.WriteLine(directoryPath);
                if (directoryPath.Equals("") || directoryPath.EndsWith("\\") || directoryPath.EndsWith("/"))
                    value += (DateTime.Now.ToString("HHmmssddmmyy") + "report-csv");
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
            System.Console.WriteLine(ReportData);
            var course = JsonConvert.DeserializeObject<Course>(ReportData);
            string csvOutput = "";
            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer))
            {
                System.Console.WriteLine("HAY is for hoarses!");

                // WRITE THE CSV HEADERS
                csv.WriteField("Course_ID");
                csv.WriteField("Module_ID");

                IEnumerable<string> ModuleItemHeaders = null;
                if (course.Modules.Count > 0)
                    if(course.Modules[0].Module_Items.Count > 0)
                    ModuleItemHeaders = course.Modules[0].Module_Items[0].GetType()
                                   .GetProperties()
                                   .ToList()
                                   .Select((property) =>
                                   {
                                       return property.Name;
                                   });
                if (ModuleItemHeaders != null)
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

    /****************************************************************************
    *                                                                           *
    *                                                                           *
    ****************************************************************************/
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
                var directoryPath = value;
                System.Console.WriteLine(directoryPath);
                if (directoryPath.Equals("") || directoryPath.EndsWith("\\") || directoryPath.EndsWith("/"))
                    value += (DateTime.Now.ToString("HHmmssddmmyy") + "report-html");
                if (!value.Contains(".html"))
                    _Destination = value + ".html";
                else
                    _Destination = value;
            }
        }

        private string PathToBoilerplate;
        public GenerateHTML() { }
        public GenerateHTML(string dest, string PathToBoilerplate)
        {
            this.Destination = dest;
            this.PathToBoilerplate = PathToBoilerplate;
        }

        public string Format { get; set; }

        public bool GenerateReport(string ReportData, string[] headers)
        {
            return GenerateReport(ReportData);
        }

        public bool GenerateReport(string ReportData)
        {
            string HtmlFile = "";
            var course = JsonConvert.DeserializeObject<Course>(ReportData);
            HtmlFile += GetHead();
            HtmlFile += BuildTitle(course.name, course.id);
            foreach (var module in course.Modules)
            {
                HtmlFile += BuildModule(module);
            }
            HtmlFile += "</body>";
            try
            {
                File.WriteAllText(this.Destination, HtmlFile);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private string GetHead()
        {
            return File.ReadAllText(PathToBoilerplate);
        }

        private string BuildTitle(string courseName, int courseId)
        {
            var titleHtml = "<div class=\"jumbotron\">" +
                "<h1 class=\"display-4\">Modules</h1>" +
                $"<h1 class=\"display-5\">{courseName}</h1>" +
                $"<h4 class=\"display-6\">{courseId}</h4>" +
                "</div>";

            return titleHtml;
        }

        private string BuildModule(Module module)
        {

            var moduleHtml =
                    $"<div class=\"card\">" +
                    $"<div class=\"card-header\" id=\"heading{module.id}\">" +
                    $"<div class=\"container\">" +
                    $"<div class=\"row\">" +
                    $"<h2 class=\"mb-0\">" +
                    $"<div class=\"col\">" +
                    $"<a class=\"btn btn-primary\" data-toggle=\"collapse\" href=\"#module{module.id}\" aria-expanded=\"false\"aria-controls=\"module{module.id}\">" +
                    $"<strong>{module.name}</strong></a>" +
                    $"</h2>" +
                    $"<div class=\"col\">" +
                        $"<p class = \"idz\">{module.id}</p>" +
                    $"</div>" +
                    $"</div>" +

                    $"</div>" +
                    $"</div>" +

                $"<div class=\"collapse\" id=\"module{module.id}\">";

            foreach (var moduleItem in module.Module_Items)
            {
                moduleHtml += BuildModuleItem(moduleItem);
            }
            moduleHtml += "</div></div>";
            return moduleHtml;
        }

        private string BuildModuleItem(Module_Item moduleItem)
        {
            var itemHtml = $"<a href=\"{moduleItem.html_url}\">" +
                "<div class=\"card\">" +
                    "<div class=\"card-body\">" +
                        $"<strong>{moduleItem.title}</strong>" +
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
            if (moduleItemType == "Page")
            {
                return "list_alt";
            }
            else if (moduleItemType == "File")
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
            else if (moduleItemType == "ExternalUrl")
            {
                return "launch";
            }
            else if (moduleItemType == "SubHeader")
            {
                return "";
            }
            else
            {
                return "assignment";
            }
        }


    }


    /****************************************************************************
    *                                                                           *
    *                                                                           *
    ****************************************************************************/
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
                var directoryPath = value;
                System.Console.WriteLine(directoryPath);
                if (directoryPath.Equals("") || directoryPath.EndsWith("\\") || directoryPath.EndsWith("/"))
                    value += (DateTime.Now.ToString("HHmmssddmmyy") + "report-json");
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

            return GenerateReport(ReportData);
        }
    }


    /****************************************************************************
    *                                                                           *
    *                                                                           *
    ****************************************************************************/
    public interface IReport
    {
        string ReportData { get; set; }
        string Destination { get; set; }

        bool GenerateReport(string ReportData);
        bool GenerateReport(string ReportData, string[] headers);
    }

}