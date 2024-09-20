using DevExpress.XtraReports.UI;
using NormalDistributionReport.Data;
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using NormalDistributionReport.Models;
using System.Reflection;
using DevExpress.ReportServer.ServiceModel.DataContracts;
using NormalDistributionReport.Reports;
using System.Linq;
using DevExpress.XtraCharts;
using static NormalDistributionReport.Models.NormalDistributionReportDTO;

namespace NormalDistributionReport.Services
{
    public class ReportService
    {

        public Dictionary<string, string> LoadTranslations(string language)
        {
            try
            {
                string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string projectDirectory = Path.GetFullPath(Path.Combine(assemblyPath, @"..\..\"));
                string path = Path.Combine(projectDirectory, "Reports", "i18n", $"{language}.json");

                Console.WriteLine($"Carregando dicionário de traduções do arquivo: {path}");

                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"Arquivo de tradução não encontrado no caminho: {path}");
                }

                string json = File.ReadAllText(path);
                var translations = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

                if (translations == null || translations.Count == 0)
                {
                    throw new Exception("O arquivo de tradução foi carregado, mas está vazio ou não pôde ser desserializado corretamente.");
                }

                Console.WriteLine($"Traduções carregadas com sucesso ({translations.Count} itens).");
                return translations;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar traduções: {ex.Message}");
                return null;
            }
        }


        public void ApplyTranslations(NormalDistributionReportDTO reportData, Dictionary<string, string> translations)
        {
            reportData.Translation.Name = translations["name"];
            reportData.Translation.Generated = translations["generated"];
            reportData.Translation.FinalScore = translations["finalScore"];
            reportData.Translation.CandidateCode = translations["candidateCode"];
            reportData.Translation.Email = translations["email"];
            reportData.Translation.Reference = translations["reference"];
            reportData.Translation.Activity = translations["activity"];
            reportData.Translation.JobOfferProcedureType = translations["jobOfferProcedureType"];
            reportData.Translation.ReportDate = translations["reportDate"];
            reportData.Translation.SelectionMethod = translations["selectionMethod"];
            reportData.Translation.Score = translations["score"];
            reportData.Translation.Notes = translations["notes"];
            reportData.Translation.Title = translations["title"];
            reportData.Translation.Description = translations["description"];
            reportData.Translation.ReportTitle = translations["reportTitle"];
            reportData.Translation.TestDate = translations["testDate"];
            reportData.Translation.NumberOfItems = translations["numberOfItems"];
            reportData.Translation.Items = translations["items"];
            reportData.Translation.PerformanceReportFor = translations["performanceReportFor"];
            reportData.Translation.Introduction = translations["introduction"];


            ApplyTranslationsToMetrics(reportData.SectionScores, reportData.Translation);
            ApplyTranslationsToMetrics(reportData.CompetenceScores, reportData.Translation);
            ApplyTranslationsToMetrics(reportData.OrganScores, reportData.Translation);
            ApplyTranslationsToMetrics(reportData.OtherScores, reportData.Translation);
        }

        private string FormatPerformanceReportTitle(NormalDistributionReportDTO reportData)
        {
            string formattedTitle = reportData.Translation.PerformanceReportFor.Replace("{StudentName}", reportData.StudentName);
            return formattedTitle;
        }

        private string FormatIntroduction(NormalDistributionReportDTO reportData)
        {
            return reportData.Translation.Introduction
                .Replace("{ExamName}", reportData.ExamName)
                .Replace("{TotalItems}", reportData.TotalItems.ToString())
                .Replace("{TotalCategories}", reportData.TotalCategories.ToString())
                .Replace("{TotalTopics}", reportData.TotalTopics.ToString());
        }

        private void ApplyTranslationsToMetrics(List<EvaluationMetrics> metrics, NormalDistributionReportTranslation translation)
        {
            foreach (var metric in metrics)
            {
                metric.Translations = translation;
            }
        }

        public static List<HistogramPoint> GenerateHistogramData(List<double> zScores, double interval)
        {
            var histogramData = new List<HistogramPoint>();

            double min = zScores.Min();
            double max = zScores.Max();

            for (double i = min; i <= max; i += interval)
            {
                double intervalCenter = i + (interval / 2);
                int count = zScores.Count(zScore => zScore >= i && zScore < i + interval);
                histogramData.Add(new HistogramPoint { IntervalCenter = intervalCenter, Count = count });
            }

            return histogramData;
        }


        public void GenerateReport(string language)
        {
            try
            {
                var reportData = ReportData.GenerateReportData();
                Console.WriteLine("Dados do relatório gerados com sucesso.");

                try
                {
                    var translations = LoadTranslations(language);
                    if (translations == null)
                    {
                        Console.WriteLine("Falha ao carregar as traduções. Continuando com valores padrão.");
                    }
                    else
                    {
                        ApplyTranslations(reportData, translations);
                        Console.WriteLine("Traduções aplicadas com sucesso.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao carregar/aplicar as traduções: {ex.Message}");
                }

                try
                {
                    XtraReport mainReport = new ReportNormalDist();
                    mainReport.DataSource = new List<NormalDistributionReportDTO> { reportData };

                    XtraReport coverReport = new Cover();
                    coverReport.DataSource = new List<NormalDistributionReportDTO> { reportData };

                    // Título do relatório
                    XRLabel performanceReportLabel = mainReport.FindControl("xrLabel1", true) as XRLabel;
                    if (performanceReportLabel != null)
                    {
                        string formattedTitle = FormatPerformanceReportTitle(reportData);
                        performanceReportLabel.Text = formattedTitle;
                        performanceReportLabel.BeforePrint += (sender, e) => {
                            (sender as XRLabel).Text = formattedTitle;
                        };
                    }
                    else
                    {
                        Console.WriteLine("Label 'xrLabel1' não encontrado no relatório!");
                    }

                    // Introdução
                    XRLabel introductionLabel = mainReport.FindControl("xrLabel2", true) as XRLabel;
                    if (introductionLabel != null)
                    {
                        string formattedIntroduction = FormatIntroduction(reportData);
                        introductionLabel.Text = formattedIntroduction;
                        introductionLabel.BeforePrint += (sender, e) => {
                            (sender as XRLabel).Text = formattedIntroduction;
                        };
                    }
                    else
                    {
                        Console.WriteLine("Label para a introdução não encontrado no relatório!");
                    }

                    // Configuração dos sub-relatórios
                    ConfigureSubReports(mainReport, reportData);

                    XtraReport finalReport;

                    if (reportData.ShowCover)
                    {
                        Console.WriteLine("Gerando capa do relatório...");

                        coverReport.CreateDocument();
                        mainReport.CreateDocument();

                        finalReport = new XtraReport();

                        Console.WriteLine($"Páginas na capa: {coverReport.Pages.Count}");
                        Console.WriteLine($"Páginas no relatório principal: {mainReport.Pages.Count}");
                        finalReport.Pages.AddRange(coverReport.Pages);
                        finalReport.Pages.AddRange(mainReport.Pages);
                        Console.WriteLine($"Relatórios combinados com sucesso. Total de páginas: {finalReport.Pages.Count}");
                    }
                    else
                    {
                        finalReport = mainReport;
                        //finalReport = coverReport;
                    }

                    string projectDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\"));
                    string pdfDirectory = Path.Combine(projectDirectory, "pdfs");

                    if (!Directory.Exists(pdfDirectory))
                    {
                        Directory.CreateDirectory(pdfDirectory);
                    }
                    else
                    {
                        Console.WriteLine($"Pasta '{pdfDirectory}' já existe.");
                    }

                    string outputPath = Path.Combine(pdfDirectory, $"Relatorio_Normal_Distribution_{language}.pdf");

                    try
                    {
                        finalReport.ExportToPdf(outputPath);
                        Console.WriteLine($"PDF gerado com sucesso em: {outputPath}");
                    }
                    catch (Exception exportEx)
                    {
                        Console.WriteLine($"Erro ao exportar o PDF: {exportEx.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao criar o relatório: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral ao gerar o relatório: {ex.Message}");
            }
        }

        private void ConfigureSubReports(XtraReport report, NormalDistributionReportDTO reportData)
        {
            //sub-relatório SectionTable
            if (reportData.SectionScores.Any())
            {
                var sectionSubReport = new Sectiontable();
                sectionSubReport.DataSource = reportData.SectionScores;

                XRSubreport subreportControl = report.FindControl("xrSubreport1", true) as XRSubreport;
                if (subreportControl != null)
                {
                    subreportControl.ReportSource = sectionSubReport;
                    Console.WriteLine("Sub-relatório de SectionScores configurado com sucesso.");
                }
            }

            // Sub-relatório chart1
            if (reportData.SectionScores.Any())
            {
                var chartSubReport = new Chart1();
                chartSubReport.DataSource = reportData.SectionScores;

                XRSubreport subreportControl = report.FindControl("xrSubreport2", true) as XRSubreport;
                if (subreportControl != null)
                {
                    subreportControl.ReportSource = chartSubReport;
                }
            }

            // Sub-relatório CompetenceTable 
            if (reportData.CompetenceScores.Any())
            {
                var competenceSubReport = new Competencetable();
                competenceSubReport.DataSource = reportData.CompetenceScores;

                XRSubreport subreportControl = report.FindControl("xrSubreport3", true) as XRSubreport;
                if (subreportControl != null)
                {
                    subreportControl.ReportSource = competenceSubReport;
                    Console.WriteLine("Sub-relatório de CompetenceScores configurado com sucesso.");
                }
            }

            // Sub-relatório Chart2
            if (reportData.CompetenceScores.Any())
            {
                var chart2SubReport = new Chart2();
                chart2SubReport.DataSource = reportData.CompetenceScores;

                XRSubreport subreportControl = report.FindControl("xrSubreport4", true) as XRSubreport;
                if (subreportControl != null)
                {
                    subreportControl.ReportSource = chart2SubReport;
                }
            }

            // Sub-relatório OrganTable 
            if (reportData.OrganScores.Any())
            {
                var OrganSubReport = new OrganTable();
                OrganSubReport.DataSource = reportData.OrganScores;

                XRSubreport subreportControl = report.FindControl("xrSubreport5", true) as XRSubreport;
                if (subreportControl != null)
                {
                    subreportControl.ReportSource = OrganSubReport;
                    Console.WriteLine("Sub-relatório de OrganScores configurado com sucesso.");
                }
            }

            // Sub-relatório Chart3
            if (reportData.OrganScores.Any())
            {
                var chart3SubReport = new Chart3();
                chart3SubReport.DataSource = reportData.OrganScores;

                XRSubreport subreportControl = report.FindControl("xrSubreport6", true) as XRSubreport;
                if (subreportControl != null)
                {
                    subreportControl.ReportSource = chart3SubReport;
                }
            }

            // Sub-relatório OthersTable 
            if (reportData.OtherScores.Any())
            {
                var OtherSubReport = new OthersTable();
                OtherSubReport.DataSource = reportData.OtherScores;

                XRSubreport subreportControl = report.FindControl("xrSubreport7", true) as XRSubreport;
                if (subreportControl != null)
                {
                    subreportControl.ReportSource = OtherSubReport;
                    Console.WriteLine("Sub-relatório de OthersScores configurado com sucesso.");
                }
            }

            // Sub-relatório Chart4
            if (reportData.OtherScores.Any())
            {
                var chart4SubReport = new Chart4();
                chart4SubReport.DataSource = reportData.OtherScores;

                XRSubreport subreportControl = report.FindControl("xrSubreport8", true) as XRSubreport;
                if (subreportControl != null)
                {
                    subreportControl.ReportSource = chart4SubReport;
                }
            }
        }
    }
}