using System;
using ProgressTestReport.Data;
using ProgressTestReport.Models; 
namespace ProgressTestReport.Services
{
    internal class ReportDataService
    {
        public ProgressTestReportDTO GetReportData()
        {
            return ReportDataGenerator.GenerateReportData(null);
        }
    }
}

