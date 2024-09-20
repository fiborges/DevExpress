using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using ProgressTestReport.Models;
using ProgressTestReport.Reports;
using ProgressTestReport.Services;
using System.Linq;

namespace ProgressTestReport.Controllers
{
    internal class ReportController
    {
        private ReportDataService _service;

        public ReportController()
        {
            _service = new ReportDataService();
        }

        public XtraReport PrepareReport()
        {
            var data = _service.GetReportData();
            var report = new CustomReport();

            
            report.DataSource = new List<ProgressTestReportDTO> { data };

            
            if (data.SectionScores.Any())
            {
                var sectionSubReport = new SectionScoresReport();
                sectionSubReport.DataSource = data.SectionScores;

                XRSubreport subreportControl = report.FindControl("xrSubreport1", true) as XRSubreport;
                if (subreportControl != null)
                {
                    subreportControl.ReportSource = sectionSubReport;
                }
            }

            
            if (data.SectionScores.Any())
            {
                var chartSubReport = new Chart1();
                chartSubReport.DataSource = data.SectionScores;

                XRSubreport subreportControl2 = report.FindControl("xrSubreport2", true) as XRSubreport;
                if (subreportControl2 != null)
                {
                    subreportControl2.ReportSource = chartSubReport;
                }
            }

            
            if (data.CompetenceScores.Any())
            {
                var competenceSubReport = new CompetenceScores();
                competenceSubReport.DataSource = data.CompetenceScores;

                XRSubreport subreportControl3 = report.FindControl("xrSubreport3", true) as XRSubreport;
                if (subreportControl3 != null)
                {
                    subreportControl3.ReportSource = competenceSubReport;
                }
            }

            
            if (data.CompetenceScores.Any())
            {
                var chartSubReport2 = new chart2();
                chartSubReport2.DataSource = data.CompetenceScores;

                XRSubreport subreportControl4 = report.FindControl("xrSubreport4", true) as XRSubreport;
                if (subreportControl4 != null)
                {
                    subreportControl4.ReportSource = chartSubReport2;
                }
            }

           
            if (data.OrganScores.Any())
            {
                var organSubReport = new OrganScores();
                organSubReport.DataSource = data.OrganScores;

                XRSubreport subreportControl5 = report.FindControl("xrSubreport5", true) as XRSubreport;
                if (subreportControl5 != null)
                {
                    subreportControl5.ReportSource = organSubReport;
                }
            }

            
            if (data.OrganScores.Any())
            {
                var chartSubReport3 = new Chart3();
                chartSubReport3.DataSource = data.OrganScores;

                XRSubreport subreportControl6 = report.FindControl("xrSubreport6", true) as XRSubreport;
                if (subreportControl6 != null)
                {
                    subreportControl6.ReportSource = chartSubReport3;
                }
            }

            
            if (data.OtherScores.Any())
            {
                var otherSubReport = new OtherScores();
                otherSubReport.DataSource = data.OtherScores;

                XRSubreport subreportControl7 = report.FindControl("xrSubreport7", true) as XRSubreport;
                if (subreportControl7 != null)
                {
                    subreportControl7.ReportSource = otherSubReport;
                }
            }

            
            if (data.OtherScores.Any())
            {
                var chartSubReport4 = new Chart4();
                chartSubReport4.DataSource = data.OtherScores;

                XRSubreport subreportControl8 = report.FindControl("xrSubreport8", true) as XRSubreport;
                if (subreportControl8 != null)
                {
                    subreportControl8.ReportSource = chartSubReport4;
                }
            }

            return report;
        }

        public void ExportReportToPDF(XtraReport report, string filePath)
        {
            report.ExportToPdf(filePath);
        }
    }
}
