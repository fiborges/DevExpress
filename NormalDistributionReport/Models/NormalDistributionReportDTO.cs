using DevExpress.Charts.Native;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;

namespace NormalDistributionReport.Models
{
    public class NormalDistributionReportDTO
    {
        public string ExamName { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public DateTime ExamDate { get; set; }
        public int TotalItems { get; set; }
        public double StudentScore { get; set; }
        public double StudentZScore { get; set; }
        public double GroupScore { get; set; }
        public int TotalCategories { get; set; }
        public int TotalTopics { get; set; }

        public double RangeValue { get; set; } = 4;
       
        public List<double> ClassFinalScores { get; set; } = new List<double>();
        public double HistogramInterval { get; set; } = 0.05;
        public bool ShowHistogram { get; set; } = true;
        public byte[] CoverImage { get; set; }
        public bool ShowCover { get; set; } = true;

        // Listas para cada subcategoria
        public List<EvaluationMetrics> SectionScores { get; set; } = new List<EvaluationMetrics>();
        public List<EvaluationMetrics> CompetenceScores { get; set; } = new List<EvaluationMetrics>();
        public List<EvaluationMetrics> OrganScores { get; set; } = new List<EvaluationMetrics>();
        public List<EvaluationMetrics> OtherScores { get; set; } = new List<EvaluationMetrics>();
        public List<HistogramPoint> HistogramDataPoints { get; set; } = new List<HistogramPoint>();


        // Dados gráfico
        public List<NormalDistributionData> GeneralNormalDistributionData { get; set; } = new List<NormalDistributionData>();
        public NormalDistributionReportTranslation Translation { get; set; } = new NormalDistributionReportTranslation();
    }

    public class EvaluationMetrics
    {
        public int EvaluationMetricsId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Items { get; set; }
        public double StudentScore { get; set; }
        public double StudentZScore { get; set; }
        public double GroupScoreAverage { get; set; }
        public NormalDistributionData NormalDistributionData { get; set; } = new NormalDistributionData();
        public string MetricName { get; set; }
        public NormalDistributionReportTranslation Translations { get; set; }
        public List<double> StudentScoresForHistogram { get; set; } = new List<double>();
        public double HistogramInterval { get; set; } = 0.05;
        public bool ShowHistogram { get; set; } = true;
        public List<HistogramPoint> HistogramDataPoints { get; set; } = new List<HistogramPoint>();
        public double RangeValue { get; set; } = 4;

    }

    public class HistogramPoint
    {
        public double IntervalCenter { get; set; }
        public int Count { get; set; }
    }

    public class DistributionPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class NormalDistributionData
    {
        public int NormalDistributionDataId { get; set; }
        public string SectionName { get; set; } = string.Empty;
        public double Mean { get; set; }
        public double StandardDeviation { get; set; }

        // Lista de pontos com coordenadas X e Y para o gráfico spline
        public List<DistributionPoint> DistributionPoints { get; set; } = new List<DistributionPoint>();
    }

    public class NormalDistributionReportTranslation
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
        public string ReportTitle { get; set; } = string.Empty;

        public string TestDate { get; set; } = string.Empty;
        public string NumberOfItems { get; set; } = string.Empty;
        public string Items { get; set; } = string.Empty;
        public string PerformanceReportFor { get; set; } = string.Empty;
        public string Introduction { get; set; } = string.Empty;
    }
}

