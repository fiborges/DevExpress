using System;
using System.Collections.Generic;
using System.IO;
using CandidateReportGenerator.Models;

namespace CandidateReportGenerator.Data
{
    public static class SampleData
    {
        public static CandidateReportDTO GetSampleCandidateReport()
        {
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string imagePath = Path.Combine(projectDirectory, "images", "logo.png");


            // Carrega a imagem como byte[]
            byte[] logoBytes = File.ReadAllBytes(imagePath);

            return new CandidateReportDTO
            {
                CandidateName = "John Doe",
                CandidateID = "123456",
                JobTitle = "Civil Engineer",
                JobCode = "CE2023",
                JobDescription = "Hiring a Civil Engineer for the city hall.",
                FinalScore = 88.0m,
                ShowFinalScore = true,
                ReportDate = DateTime.Now,
                CompanyLogo = logoBytes,

                PhaseScores = new List<PhaseMetrics>
                {
                    new PhaseMetrics
                    {
                        PhaseId = 1,
                        PhaseName = "Phase 1 - Skills Assessment",
                        Metrics = new List<EvaluationMetric>
                        {
                            new EvaluationMetric { MetricId = 1, Name = "Technical Test", Score = 10.0m, Description = "10 points - Excellent technical skills" },
                            new EvaluationMetric { MetricId = 2, Name = "Communication Skills", Score = 9.5m, Description = "9.5 points - Very good communication" }
                        }
                    },
                    new PhaseMetrics
                    {
                        PhaseId = 2,
                        PhaseName = "Phase 2 - Psychological Test",
                        Metrics = new List<EvaluationMetric>
                        {
                            new EvaluationMetric { MetricId = 3, Name = "Stress Management", Score = 8.0m, Description = "8 points - Good under pressure" },
                            new EvaluationMetric { MetricId = 4, Name = "Problem-Solving", Score = 9.0m, Description = "9 points - Excellent problem-solving skills" }
                        }
                    },
                    new PhaseMetrics
                    {
                        PhaseId = 3,
                        PhaseName = "Phase 3 - Final Interview",
                        Metrics = new List<EvaluationMetric>
                        {
                            new EvaluationMetric { MetricId = 5, Name = "Interview Performance", Score = 9.0m, Description = "9 points - Strong performance" },
                            new EvaluationMetric { MetricId = 6, Name = "Team Fit", Score = 8.5m, Description = "8.5 points - Well-suited for the team" }
                        }
                    }
                }
            };
        }
    }
}
