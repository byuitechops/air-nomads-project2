using System;
using System.Net.Http;
using System.Threading.Tasks;
namespace air_nomades_projectSquared
{
    interface HttpObject
    {
        string URL { get; set; }
        string Token { get; set; }
        // Task GetRequest();
    }

    class GET : HttpObject
    {
        public string URL { get; set; }
        public string Token { get; set; }
        public GET()
        {
            Token = Environment.GetEnvironmentVariable("API_TOKEN");
        }
        internal async Task<string> MakeGetRequest(Prompt Prompt)
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
                    HttpResponseMessage response = await client.GetAsync(URL);
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


    class POST : HttpObject
    {
        public string URL { get; set; }
        public string Token { get; set; }
        // string token = Environment.GetEnvironmentVariable("API_TOKEN");
        public async Task MakePostRequest()
        {

        }
    }
}