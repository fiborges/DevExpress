using CandidateReportDesign.Data;
using CandidateReportDesign.Models;
using CandidateReportDesign.Services;
using DevExpress.XtraReports.UI;

class Program
{
    static void Main(string[] args)
    {
        var data = DataGenerator.GenerateData();
        var reportGenerator = new CandidateResultsReportGenerator();

        reportGenerator.GenerateAndSavePDF(data, "en-US");  // Use "pt-PT" para português ou outra string conforme necessário
    }
}
