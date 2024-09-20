using System;
using System.IO;
using DevExpress.XtraReports.UI;
using ProgressTestReport.Controllers;

namespace ProgressTestReport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ReportController controller = new ReportController();
                XtraReport report = controller.PrepareReport();

                string projectRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..", "ProgressTestReport");
                string pdfPath = Path.Combine(projectRoot, "pdfs", "report.pdf");

                report.ExportToPdf(pdfPath);


                controller.ExportReportToPDF(report, pdfPath);

                Console.WriteLine("Relatório gerado com sucesso em: " + pdfPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
            Console.ReadLine();
        }
    }
}
