using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using ProgressTestReport.Models;

namespace ProgressTestReport.Data
{
    public static class ReportDataGenerator
    {
        public static ProgressTestReportDTO GenerateReportData(ILogger logger)
        {
            var sectionScores = new List<EvaluationMetrics>
            {
                new EvaluationMetrics
                {
                    EvaluationMetricsId = 1,
                    Name = "Parte 1",
                    Items = 12,
                    StudentScore = 11.7,
                    GroupScoreAverage = 9.0,
                    BoxPlotData = new BoxPlotData
                    {
                        BoxPlotDataId = 1,
                        SectionName = "Parte 1",
                        Min = 7.5,
                        Q1 = 8.0,
                        Median = 9.0,
                        Q3 = 10.5,
                        Max = 12.0
                    }
                },
                new EvaluationMetrics
                {
                    EvaluationMetricsId = 2,
                    Name = "Parte 2",
                    Items = 26,
                    StudentScore = 13.8,
                    GroupScoreAverage = 10.2,
                    BoxPlotData = new BoxPlotData
                    {
                        BoxPlotDataId = 2,
                        SectionName = "Parte 2",
                        Min = 9.5,
                        Q1 = 10.0,
                        Median = 10.5,
                        Q3 = 12.0,
                        Max = 13.5
                    }
                },
                new EvaluationMetrics
                {
                    EvaluationMetricsId = 3,
                    Name = "Parte 3",
                    Items = 27,
                    StudentScore = 13.3,
                    GroupScoreAverage = 10.3,
                    BoxPlotData = new BoxPlotData
                    {
                        BoxPlotDataId = 3,
                        SectionName = "Parte 3",
                        Min = 9.0,
                        Q1 = 10.0,
                        Median = 11.0,
                        Q3 = 12.0,
                        Max = 14.0
                    }
                },
                new EvaluationMetrics
                {
                    EvaluationMetricsId = 4,
                    Name = "Parte 4",
                    Items = 13,
                    StudentScore = 10.8,
                    GroupScoreAverage = 7.3,
                    BoxPlotData = new BoxPlotData
                    {
                        BoxPlotDataId = 4,
                        SectionName = "Parte 4",
                        Min = 6.5,
                        Q1 = 7.0,
                        Median = 8.0,
                        Q3 = 9.0,
                        Max = 11.0
                    }
                },
                new EvaluationMetrics
                {
                    EvaluationMetricsId = 5,
                    Name = "Parte 5",
                    Items = 10,
                    StudentScore = 10.0,
                    GroupScoreAverage = 11.8,
                    BoxPlotData = new BoxPlotData
                    {
                        BoxPlotDataId = 5,
                        SectionName = "Parte 5",
                        Min = 8.0,
                        Q1 = 9.0,
                        Median = 10.0,
                        Q3 = 11.5,
                        Max = 13.0
                    }
                }
            };

            var competenceScores = new List<EvaluationMetrics>
            {
                new EvaluationMetrics
                {
                    EvaluationMetricsId = 6,
                    Name = "Princípios Científicos",
                    Items = 26,
                    StudentScore = 11.1,
                    GroupScoreAverage = 9.0,
                    BoxPlotData = new BoxPlotData
                    {
                        BoxPlotDataId = 6,
                        SectionName = "Princípios Científicos",
                        Min = 8.0,
                        Q1 = 9.0,
                        Median = 10.0,
                        Q3 = 11.0,
                        Max = 12.0
                    }
                },
                new EvaluationMetrics
                {
                    EvaluationMetricsId = 7,
                    Name = "Tratamento",
                    Items = 13,
                    StudentScore = 7.7,
                    GroupScoreAverage = 7.8,
                    BoxPlotData = new BoxPlotData
                    {
                        BoxPlotDataId = 7,
                        SectionName = "Tratamento",
                        Min = 6.0,
                        Q1 = 7.0,
                        Median = 8.0,
                        Q3 = 8.5,
                        Max = 10.0
                    }
                },
                new EvaluationMetrics
                {
                    EvaluationMetricsId = 8,
                    Name = "Diagnóstico",
                    Items = 15,
                    StudentScore = 12.9,
                    GroupScoreAverage = 12.2,
                    BoxPlotData = new BoxPlotData
                    {
                        BoxPlotDataId = 8,
                        SectionName = "Diagnóstico",
                        Min = 10.0,
                        Q1 = 11.0,
                        Median = 12.0,
                        Q3 = 13.0,
                        Max = 15.0
                    }
                },
                new EvaluationMetrics
                {
                    EvaluationMetricsId = 9,
                    Name = "Fisiopatologia",
                    Items = 6,
                    StudentScore = 16.7,
                    GroupScoreAverage = 12.7,
                    BoxPlotData = new BoxPlotData
                    {
                        BoxPlotDataId = 9,
                        SectionName = "Fisiopatologia",
                        Min = 14.0,
                        Q1 = 15.0,
                        Median = 16.0,
                        Q3 = 17.0,
                        Max = 20.0
                    }
                }
            };

            var organScores = new List<EvaluationMetrics>
            {
                new EvaluationMetrics
                {
                    EvaluationMetricsId = 10,
                    Name = "Sistema Imunitário",
                    Items = 13,
                    StudentScore = 13.8,
                    GroupScoreAverage = 10.2,
                    BoxPlotData = new BoxPlotData
                    {
                        BoxPlotDataId = 10,
                        SectionName = "Sistema Imunitário",
                        Min = 12.0,
                        Q1 = 13.0,
                        Median = 14.0,
                        Q3 = 15.0,
                        Max = 18.0
                    }
                },
                new EvaluationMetrics
                {
                    EvaluationMetricsId = 11,
                    Name = "Tecido Subcutâneo",
                    Items = 4,
                    StudentScore = 20.0,
                    GroupScoreAverage = 11.2,
                    BoxPlotData = new BoxPlotData
                    {
                        BoxPlotDataId = 11,
                        SectionName = "Tecido Subcutâneo",
                        Min = 18.0,
                        Q1 = 19.0,
                        Median = 20.0,
                        Q3 = 21.0,
                        Max = 23.0
                    }
                },
                new EvaluationMetrics
                {
                    EvaluationMetricsId = 12,
                    Name = "Sistema Endócrino",
                    Items = 23,
                    StudentScore = 11.3,
                    GroupScoreAverage = 9.2,
                    BoxPlotData = new BoxPlotData
                    {
                        BoxPlotDataId = 12,
                        SectionName = "Sistema Endócrino",
                        Min = 9.0,
                        Q1 = 10.0,
                        Median = 11.0,
                        Q3 = 12.0,
                        Max = 14.0
                    }
                },
                new EvaluationMetrics
                {
                    EvaluationMetricsId = 13,
                    Name = "Sistema Gastrointestinal",
                    Items = 68,
                    StudentScore = 18.8,
                    GroupScoreAverage = 13.2,
                    BoxPlotData = new BoxPlotData
                    {
                        BoxPlotDataId = 13,
                        SectionName = "Sistema Gastrointestinal",
                        Min = 15.0,
                        Q1 = 16.0,
                        Median = 17.0,
                        Q3 = 18.0,
                        Max = 22.0
                    }
                },
                new EvaluationMetrics
                {
                    EvaluationMetricsId = 14,
                    Name = "Sistema Respiratório",
                    Items = 6,
                    StudentScore = 16.7,
                    GroupScoreAverage = 12.6,
                    BoxPlotData = new BoxPlotData
                    {
                        BoxPlotDataId = 14,
                        SectionName = "Sistema Respiratório",
                        Min = 12.0,
                        Q1 = 13.0,
                        Median = 14.0,
                        Q3 = 15.0,
                        Max = 17.0
                    }
                }
            };

            var otherScores = new List<EvaluationMetrics>
            {
                //teste de lista vazia
            };

            return new ProgressTestReportDTO
            {
                StudentName = "Maria Almeida",
                ExamDate = DateTime.Parse("2024-06-04"),
                TotalItems = sectionScores.Sum(x => x.Items) + competenceScores.Sum(x => x.Items) +
                             organScores.Sum(x => x.Items) + otherScores.Sum(x => x.Items),
                StudentScore = 13.5,
                GroupScore = 10.0,
                TotalCategories = 4,
                TotalTopics = sectionScores.Count + competenceScores.Count + organScores.Count + otherScores.Count,
                SectionScores = sectionScores,
                CompetenceScores = competenceScores,
                OrganScores = organScores,
                OtherScores = otherScores,
                GeneralBoxPlotData = new List<BoxPlotData>
                {
                    new BoxPlotData
                    {
                        BoxPlotDataId = 16,
                        SectionName = "Geral",
                        Min = 6.5,
                        Q1 = 8.0,
                        Median = 10.0,
                        Q3 = 12.0,
                        Max = 14.0
                    }
                }
            };
        }
    }
}
