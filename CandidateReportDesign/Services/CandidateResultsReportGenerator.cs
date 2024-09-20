using CandidateReportDesign.Models;
using CandidateReportDesign.Reports;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace CandidateReportDesign.Services
{
    internal class CandidateResultsReportGenerator
    {
        public void GenerateAndSavePDF(CandidateResultsReportDTO candidateResults, string language)
        {
            Console.WriteLine("Starting the report generation process...");
            var report = new CandidateReport();  // Assume que você tem uma classe personalizada do relatório aqui.
            var translations = LoadTranslations(language);

            ApplyTranslations(candidateResults, translations);
            report.DataSource = new List<CandidateResultsReportDTO> { candidateResults };

            SavePdfToFile(report);
        }

        private Dictionary<string, string> LoadTranslations(string language)
        {
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine($"Assembly Path2: {assemblyPath}"); // Debug: Mostra o caminho do assembly

            string projectDirectory = Path.GetFullPath(Path.Combine(assemblyPath, @"..\..\"));
            Console.WriteLine($"Project Directory2: {projectDirectory}"); // Debug: Mostra o caminho do diretório do projeto

            string path = Path.Combine(projectDirectory, "Reports", "i18n", $"{language}.json");
            Console.WriteLine($"Expected JSON Path2: {path}"); // Debug: Mostra o caminho esperado do JSON

            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist."); // Debug: Indica se o arquivo não existe
                throw new FileNotFoundException($"Could not find the file at {path}");
            }

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }

        private void ApplyTranslations(CandidateResultsReportDTO candidateResults, Dictionary<string, string> translations)
        {
            if (candidateResults.Translation == null)
                candidateResults.Translation = new CandidateResultsReportTranslation();

            candidateResults.Translation.Name = translations["name"];
            candidateResults.Translation.Generated = translations["generated"];
            candidateResults.Translation.FinalScore = translations["finalScore"];
            candidateResults.Translation.CandidateCode = translations["candidateCode"];
            candidateResults.Translation.Email = translations["email"];
            candidateResults.Translation.Reference = translations["reference"];
            candidateResults.Translation.Activity = translations["activity"];
            candidateResults.Translation.JobOfferProcedureType = translations["jobOfferProcedureType"];
            candidateResults.Translation.ReportDate = translations["reportDate"];
            candidateResults.Translation.SelectionMethod = translations["selectionMethod"];
            candidateResults.Translation.Score = translations["score"];
            candidateResults.Translation.Notes = translations["notes"];
            candidateResults.Translation.Title = translations["title"];
            candidateResults.Translation.Description = translations["description"];
        }

        private void SavePdfToFile(XtraReport report)
        {
            string pdfDirectory = @"C:\Users\joaoa\OneDrive\Ambiente de Trabalho\CandidateReportDesign\pdfs";
            string filePath = Path.Combine(pdfDirectory, "CandidateResultsReport.pdf");

            Console.WriteLine($"Saving PDF to: {filePath}");
            report.ExportToPdf(filePath);
            Console.WriteLine($"PDF successfully saved to: {filePath}");
        }
    }
}

