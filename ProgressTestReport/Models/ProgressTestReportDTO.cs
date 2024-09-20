using System;
using System.Collections.Generic;

namespace ProgressTestReport.Models
{
    public class ProgressTestReportDTO
    {
        public string StudentName { get; set; } = string.Empty;
        public DateTime ExamDate { get; set; }
        public int TotalItems { get; set; }
        public double StudentScore { get; set; }
        public double GroupScore { get; set; }
        public int TotalCategories { get; set; }
        public int TotalTopics { get; set; }

        // Listas para cada subcategoria
        public List<EvaluationMetrics> SectionScores { get; set; } = new List<EvaluationMetrics>();
        public List<EvaluationMetrics> CompetenceScores { get; set; } = new List<EvaluationMetrics>();
        public List<EvaluationMetrics> OrganScores { get; set; } = new List<EvaluationMetrics>();
        public List<EvaluationMetrics> OtherScores { get; set; } = new List<EvaluationMetrics>();

        // Propriedade para o Box Plot geral
        public List<BoxPlotData> GeneralBoxPlotData { get; set; } = new List<BoxPlotData>();
    }

    public class EvaluationMetrics
    {
        public int EvaluationMetricsId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Items { get; set; }
        public double StudentScore { get; set; }
        public double GroupScoreAverage { get; set; }
        public BoxPlotData BoxPlotData { get; set; } = new BoxPlotData();
    }

    public class BoxPlotData
    {
        public int BoxPlotDataId { get; set; }
        public string SectionName { get; set; } = string.Empty;
        public double Min { get; set; }
        public double Q1 { get; set; }
        public double Median { get; set; }
        public double Q3 { get; set; }
        public double Max { get; set; }
    }
}