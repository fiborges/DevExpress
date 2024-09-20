using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace ProgressTestReport.Reports
{
    public partial class CustomReport : DevExpress.XtraReports.UI.XtraReport
    {
        public CustomReport()
        {
            InitializeComponent();
        }

        private void xrChart1_BeforePrint(object sender, CancelEventArgs e)
        {
            var report = this;
            var chart = report.FindControl("xrChart1", true) as XRChart;

            if (chart != null)
            {
                var studentScore = Convert.ToDouble(report.GetCurrentColumnValue("StudentScore"));
                
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
    }
}
