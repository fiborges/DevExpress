using CandidateReportGenerator.Data;
using CandidateReportGenerator.Models;

namespace CandidateReportGenerator.Services
{
    public class CandidateService
    {
        public CandidateReportDTO GetCandidateReportData()
        {
            return SampleData.GetSampleCandidateReport();
        }
    }
}
