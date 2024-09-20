using System;
using System.Collections.Generic;
using NormalDistributionReport.Services;
using NormalDistributionReport.Models;
using DevExpress.XtraReports.UI;
using System.IO;

namespace NormalDistributionReport.Controllers
{
    internal class ReportController
    {
        private readonly ReportDataService _reportDataService;
        private readonly TranslationService _translationService;

        public ReportController(ReportDataService reportDataService, TranslationService translationService)
        {
            _reportDataService = reportDataService;
            _translationService = translationService;
        }

        public XtraReport PrepareReport(string language)
        {
            Console.WriteLine("Preparando o relatório...");

            NormalDistributionReportDTO reportData = _reportDataService.GetReportData();
            Console.WriteLine("Dados do relatório obtidos com sucesso.");

            var translations = _translationService.LoadTranslations(language);
            _translationService.ApplyTranslations(reportData, translations);
            Console.WriteLine($"Traduções aplicadas para o idioma: {language}");

            var report = new Reports.NormalDistributionReport();
            report.DataSource = new List<NormalDistributionReportDTO> { reportData };

            return report;
        }

        public void GenerateAndSaveReport(string outputPath, string language)
        {
            try
            {
                var report = PrepareReport(language);
                string fileName = GenerateFileName();
                string fullPath = Path.Combine(outputPath, fileName);

                Console.WriteLine($"Iniciando exportação do PDF para: {fullPath}");
                report.ExportToPdf(fullPath);
                Console.WriteLine("Exportação do PDF concluída.");

                if (new FileInfo(fullPath).Length == 0)
                {
                    throw new Exception("O arquivo PDF gerado está vazio");
                }

                Console.WriteLine($"Relatório salvo com sucesso em: {fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerar o relatório PDF: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        private string GenerateFileName()
        {
            return $"NormalDistributionReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
        }
    }
}