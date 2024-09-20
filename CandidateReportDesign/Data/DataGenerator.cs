using CandidateReportDesign.Models;
using System;
using System.Collections.Generic;

namespace CandidateReportDesign.Data
{
    internal class DataGenerator
    {
        public static CandidateResultsReportDTO GenerateData()
        {
            return new CandidateResultsReportDTO
            {
                CandidateName = "John Doe",
                CandidateCode = "JD2024",
                CandidateEmail = "john.doe@example.com",
                Reference = "REF1234",
                ReferenceYear = "2024",
                Activity = "Software Development",
                JobOfferProcedureType = "Full-time",
                FinalScore = 88.5m,
                ShowFinalScore = true,
                ReportDate = DateTime.Now,
                SelectionMethods = new List<SelectionMethod>
                {
                    new SelectionMethod
                    {
                        Id = "PH1",
                        Title = "Phase 1 - Skills Assessment",
                        Description = "Technical and Communication Skills Evaluation",
                        Code = "SKILLS",
                        Metrics = new List<SelectionMethodMetric>
                        {
                            new SelectionMethodMetric { Title = "Technical Test", Code = "TECH", Score = 10.0m, ScoreDescription = "10 points - Excellent technical skills" },
                            new SelectionMethodMetric { Title = "Communication Skills", Code = "COMM", Score = 9.5m, ScoreDescription = "9.5 points - Very good communication" }
                        }
                    },
                    new SelectionMethod
                    {
                        Id = "PH2",
                        Title = "Phase 2 - Psychological Test",
                        Description = "Assessment of Psychological Traits",
                        Code = "PSYCH",
                        Metrics = new List<SelectionMethodMetric>
                        {
                            new SelectionMethodMetric { Title = "Stress Management", Code = "STRESS", Score = 8.0m, ScoreDescription = "8 points - Good under pressure" },
                            new SelectionMethodMetric { Title = "Problem-Solving", Code = "PROBSOLV", Score = 9.0m, ScoreDescription = "9 points - Excellent problem-solving skills" }
                        }
                    },
                    new SelectionMethod
                    {
                        Id = "PH3",
                        Title = "Phase 3 - Final Interview",
                        Description = "Comprehensive Interview by Panel",
                        Code = "INTERVIEW",
                        Metrics = new List<SelectionMethodMetric>
                        {
                            new SelectionMethodMetric { Title = "Interview Performance", Code = "PERF", Score = 9.0m, ScoreDescription = "9 points - Strong performance" },
                            new SelectionMethodMetric { Title = "Team Fit", Code = "TEAMFIT", Score = 8.5m, ScoreDescription = "8.5 points - Well-suited for the team" }
                        }
                    }
                }
            };
        }
    }
}
