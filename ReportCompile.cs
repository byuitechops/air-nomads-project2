
using System.Threading.Tasks;
using AirNomadHttpGrabers;
using AirNomadPrompter;
using ReportGeneratorFunctions;

namespace AirNomadReportCompile
{
    public class ReportCompile
    {
        private IReport ReportGenerator;
        private HttpObject HttpHandler;
        private Prompt PromptObject;
        public ReportCompile() { }
        public ReportCompile(Prompt prompt, IReport ReportGenerator, HttpObject HttpHandler)
        {
            this.CalibrateCompiler(prompt, ReportGenerator, HttpHandler);
        }
        public void CalibrateCompiler(Prompt prompt, IReport ReportGenerator, HttpObject HttpHandler)
        {
            this.ReportGenerator = ReportGenerator;
            this.HttpHandler = HttpHandler;
            this.PromptObject = prompt;
        }

        public async Task<bool> CompileReport()
        {
            var request = await this.HttpHandler.grabCourseData();
            var result = this.ReportGenerator.GenerateReport(request);
            return result;
        }

        // public async Task Compile()
        // {
        //     var request = await HttpHandler.GetRequest();
        //     return ReportGenerator.GenerateReport(request);

        // }
    }
}