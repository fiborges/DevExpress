namespace CandidateReportGenerator.Models
{
    public class Metric
    {
        public string Title { get; set; } // Title of the metric (e.g., Skills Test)
        public decimal Score { get; set; } // Score obtained in the metric
        public string Description { get; set; } // Description of the score
    }
}