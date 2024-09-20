using DevExpress.Charts;
using DevExpress.XtraReports.UI;
using ProgressTestReport.Models;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraCharts;

namespace ProgressTestReport.Reports
{
    public partial class ProgressTestPerformanceReport : DevExpress.XtraReports.UI.XtraReport
    {
        public ProgressTestPerformanceReport()
        {
            InitializeComponent();
        }

        private void xrChart1_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var report = this;
            var chart = report.FindControl("xrChart1", true) as XRChart;

            if (chart != null)
            {
                var studentScore = Convert.ToDouble(report.GetCurrentColumnValue("StudentScore"));
                // Assuming there is a way to set the constant line value directly in DevExpress.Charts
                var constantLine = new ConstantLine
                {
                    AxisValue = studentScore,
                    Color = Color.FromArgb(149, 179, 215)
                };

                if (chart.Diagram is XYDiagram diagram)
                {
                    diagram.AxisY.ConstantLines.Clear();
                    diagram.AxisY.ConstantLines.Add(constantLine);
                }
            }
            else
            {
                Debug.WriteLine("**chart** was null.");
            }
        }

       /* private void Detail3_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
        {
            XRSubreport? subreportControl = sender as XRSubreport;
            Debug.WriteLine("Subreport control: " + subreportControl);

            if (subreportControl != null)
            {
                SectionSubReport? subreport = subreportControl.ReportSource as SectionSubReport;
                Debug.WriteLine("Subreport: " + subreport);
                if (subreport != null)
                {
                    subreport.DataSource = this.DataSource;
                    subreport.DataMember = "SectionScores";
                    Debug.WriteLine("Subrelatório e DataSource atribuídos corretamente.");
                }
                else
                {
                    Debug.WriteLine("O subreport é null ou não é do tipo esperado.");
                }
            }
            else
            {
                Debug.WriteLine("O sender não é do tipo XRSubreport.");
            }
        }*/
    }
}
