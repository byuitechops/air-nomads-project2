using System.Threading.Tasks;

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

    public async Task GetRequest()
    {
        // DO SUPER LEGIT ASYNC REQUEST STUFF HERE
    }
}


class POST : HttpObject
{
    public string URL { get; set; }
    public string Token { get; set; }

    public async Task GetRequest()
    {
    }
}