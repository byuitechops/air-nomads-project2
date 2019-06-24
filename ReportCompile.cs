
using System.Threading.Tasks;

class ReportCompile{
    private IReport ReportGenerator;
    private HttpObject HttpHandler;
    private Prompt PromptObject;
    public ReportCompile(Prompt prompt, IReport ReportGenerator, HttpObject HttpHandler){
        this.ReportGenerator = ReportGenerator;
        this.HttpHandler = HttpHandler; 
        this.PromptObject = prompt;        
    }

    public async Task Compile(){
        var request = await HttpHandler.GetRequest();
        return ReportGenerator.GenerateReport(request);
        
    }
}