using System;
using System.Collections.Generic;

namespace CandidateReportGenerator.Models
{
    public class CandidateReportDTO
    {
        public string CandidateName { get; set; } = string.Empty;
        public string CandidateID { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string JobCode { get; set; } = string.Empty;
        public string JobDescription { get; set; } = string.Empty;
        public decimal FinalScore { get; set; }
        public bool ShowFinalScore { get; set; } = true;
        public DateTime ReportDate { get; set; }
        public byte[] CompanyLogo { get; set; } = Array.Empty<byte>();


        public List<PhaseMetrics> PhaseScores { get; set; } = new List<PhaseMetrics>();
    }

    public class PhaseMetrics
    {
        public int PhaseId { get; set; }
        public string PhaseName { get; set; } = string.Empty;
        public List<EvaluationMetric> Metrics { get; set; } = new List<EvaluationMetric>();
    }

    public class EvaluationMetric
    {
        public int MetricId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Score { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}