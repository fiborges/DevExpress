using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateReportDesign.Models
{
    public class CandidateResultsReportDTO
    {
        public string CandidateName { get; set; } = string.Empty;
        public string CandidateCode { get; set; } = string.Empty;
        public string CandidateEmail { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public string ReferenceYear { get; set; } = string.Empty;
        public string Activity { get; set; } = string.Empty;
        public string JobOfferProcedureType { get; set; } = string.Empty;
        public decimal FinalScore { get; set; }
        public bool ShowFinalScore { get; set; } = true;
        public DateTime ReportDate { get; set; }
        public List<SelectionMethod> SelectionMethods { get; set; } = new List<SelectionMethod>();
        public CandidateResultsReportTranslation Translation { get; set; }
    }

    public class SelectionMethod
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public decimal? Score { get; set; }
        public List<SelectionMethodMetric> Metrics { get; set; } = new List<SelectionMethodMetric>();
    }

    public class SelectionMethodMetric
    {
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public decimal? Score { get; set; }
        public string ScoreDescription { get; set; } = string.Empty;
    }

    public class CandidateResultsReportTranslation
    {
        public string Name { get; set; } = string.Empty;
        public string Generated { get; set; } = string.Empty;
        public string CandidateCode { get; set; } = string.Empty;
        public string FinalScore { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public string Activity { get; set; } = string.Empty;
        public string JobOfferProcedureType { get; set; } = string.Empty;
        public string ReportDate { get; set; } = string.Empty;
        public string SelectionMethod { get; set; } = string.Empty;
        public string Score { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
