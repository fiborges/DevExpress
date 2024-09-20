using System.Collections.Generic;

namespace CandidateReportGenerator.Models
{
    public class Phase
    {
        public string Title { get; set; } // Title of the phase
        public string Code { get; set; } // Code of the phase
        public List<Metric> Metrics { get; set; } // List of metrics for this phase
    }
}