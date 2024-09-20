using System.Collections.Generic;
using System.Security.Policy;

namespace CandidateReportGenerator.Models
{
    public class Candidate
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ID { get; set; } // Candidate's ID or number
        public string JobTitle { get; set; } // Job title the candidate is applying for
        public string JobCode { get; set; } // Code of the job procedure
        public string JobDescription { get; set; } // Description of the job procedure
        public decimal FinalScore { get; set; } // Final score of the candidate

        public bool ShowFinalScore { get; set; } // Flag to determine if the final score should be printed

        public List<Phase> Phases { get; set; } // List of phases the candidate has gone through
    }
}