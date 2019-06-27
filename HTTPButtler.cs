using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CanvasObjects;
using Newtonsoft.Json;
namespace air_nomades_projectSquared
{
    public abstract class HttpObject
    {
        public string Token { get; set; }
        private static readonly HttpClient client = new HttpClient();
        public HttpObject()
        {
            Token = Environment.GetEnvironmentVariable("API_TOKEN");
        }
        #pragma warning disable 1998
        public virtual async Task<string> grabCourseData()
        {
            return "{\"result\":\"No Data to Grab!\"}";
        }
        virtual public async Task<string> MakeGetRequest(string url)
        {
            try
            {
                //Sets securely our canvas token to our http header
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
                //asynchronously makes a get request to the link we want to
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                //stringfy the response
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }

        }
    }


    public class CourseGrabber : HttpObject
    {
        public string CourseID;
        public Course CourseObject;
        private ModuleGrabber ModGrabber;

        private void setup()
        {
            this.ModGrabber = new ModuleGrabber();
        }
        public CourseGrabber()
        {
            setup();
        }
        public CourseGrabber(string CourseID)
        {
            this.CourseID = CourseID;
            setup();
        }
        // Grabs the course with all of its modules and their items.
        public async override Task<string> grabCourseData()
        {
            var url = "https://byui.instructure.com/api/v1/courses/" + this.CourseID;
            var result = await this.MakeGetRequest(url);
            this.CourseObject = JsonConvert.DeserializeObject<Course>(result);
            this.CourseObject.Modules = await ModGrabber.getModules(url);
            var json = JsonConvert.SerializeObject(this.CourseObject, Formatting.Indented);

            return json;
        }

    }

    public class DoNothingGrabber : HttpObject
    {

    }

    public class GetAccountHandler : HttpObject
    {
        public override async Task<string> grabCourseData()
        {
            return await this.MakeGetRequest("https://byui.instructure.com/api/v1/accounts");
        }
    }

    public class ModuleItemGrabber : HttpObject
    {

        public ModuleItemGrabber()
        {

        }
        public async Task<List<Module_Item>> getModuleItems(string url)
        {
            string result = await this.MakeGetRequest(url);
            if (!result.StartsWith("["))
                result = $"[{result}]";
            var items = JsonConvert.DeserializeObject<List<Module_Item>>(result);
            return items;
        }
    }
    public class ModuleGrabber : HttpObject
    {

        private ModuleItemGrabber ItemGrabber { get; set; }
        public ModuleGrabber()
        {
            this.ItemGrabber = new ModuleItemGrabber();
        }

        public async Task<List<Module>> getModules(string url)
        {
            var requestUrl = url + "/modules";
            var result = await this.MakeGetRequest(requestUrl);
            var Modules = JsonConvert.DeserializeObject<List<Module>>(result);

            foreach (var module in Modules)
            {
                System.Console.WriteLine(module.items_url);
                module.Module_Items = (await ItemGrabber.getModuleItems(module.items_url));
            }
            return Modules;
        }
    }

}
