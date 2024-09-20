using System;
using System.Collections.Generic;
using CandidateReportGenerator.Services;
using CandidateReportGenerator.Reports;
using DevExpress.XtraReports.UI;
using CandidateReportGenerator.Models;
using System.IO;

namespace CandidateReportGenerator.Controllers
{
    public class CandidateController
    {
        private readonly CandidateService _candidateService;
        private readonly ReportController _reportController;

        public CandidateController()
        {
            _candidateService = new CandidateService();
            _reportController = new ReportController();
        }

        public void GenerateReport(List<int> selectedPhases)
        {
            try
            {
                Console.WriteLine("Starting report generation...");

                
                var candidateReportData = new List<CandidateReportDTO> { _candidateService.GetCandidateReportData() };

               
                XtraReport report = _reportController.CreateCandidateReport(candidateReportData, selectedPhases);

                Console.WriteLine("Report created and configured.");

              
                string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
                string pdfDirectory = Path.Combine(projectDirectory, "pdfs");

                if (!Directory.Exists(pdfDirectory))
                {
                    Console.WriteLine($"Creating directory: {pdfDirectory}");
                    Directory.CreateDirectory(pdfDirectory);
                }
                string outputPath = Path.Combine(pdfDirectory, "CandidateReport.pdf");
                Console.WriteLine($"Output path set to: {outputPath}");

                report.ExportToPdf(outputPath);
                Console.WriteLine($"Report exported to PDF: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
