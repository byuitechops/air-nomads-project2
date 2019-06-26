using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CanvasObjects;
namespace air_nomades_projectSquared
{
    public abstract class HttpObject
    {
        public string URL { get; set; }
        public string Token { get; set; }
        public HttpObject()
        {
            Token = Environment.GetEnvironmentVariable("API_TOKEN");
        }
        virtual internal async Task<string> MakeGetRequest(Prompt Prompt, string URL2)
        {
            //DEFINE THE URL FOR THE CALL BY USING THE PROMPT OBJECT
            System.Console.WriteLine(Prompt);
            // DO SUPER LEGIT ASYNC REQUEST STUFF HERE
            using (HttpClient client = new HttpClient())
            {

                try
                {
                    //Sets securely our canvas token to our http header
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
                    //asynchronously makes a get request to the link we want to
                    HttpResponseMessage response = await client.GetAsync(URL2);
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
    }

    public class CourseGrabber:HttpClient{
       public int CourseID;
       public CourseGrabber(int CourseID){
           this.CourseID = CourseID;
       }

        public Course grabCourse(ModuleGrabber ModuleGrabber, ModuleItemGrabber ItemGrabber){

        }

    }

    public class ModuleItemGrabber : HttpClient{

        public ModuleItemGrabber(){

        }
        public List<Module_Item> getModuleItems(string[] module_ids){
            var items = new List<Module_Item>();
            return items;
        }
    }
    public class ModuleGrabber : HttpObject{
        public List<Module> getModules(string[] module_ids){
            var modules = new List<Module>();
            return modules;
        }
    }

}
