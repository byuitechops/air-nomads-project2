
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
       
        public ReportCompile() { }
        public ReportCompile( IReport ReportGenerator, HttpObject HttpHandler)
        {
            this.CalibrateCompiler( ReportGenerator, HttpHandler);
        }
        public void CalibrateCompiler(IReport ReportGenerator, HttpObject HttpHandler)
        {
            this.ReportGenerator = ReportGenerator;
            this.HttpHandler = HttpHandler;
        }
        public void CalibrateCompiler(Prompt p, IReport ReportGenerator, HttpObject HttpHandler)
        {
            this.ReportGenerator = ReportGenerator;
            this.HttpHandler = HttpHandler;
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