using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using ProgressTestReport.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ProgressTestReport.Reports
{
    public partial class Chart4 : DevExpress.XtraReports.UI.XtraReport
    {
        public Chart4()
        {
            InitializeComponent();
            this.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(xrChart1_BeforePrint);
        }

        private void xrChart1_BeforePrint(object sender, CancelEventArgs e)
        {
            XRChart chart = this.FindControl("xrChart1", true) as XRChart;

            if (chart != null)
            {
                
                var data = this.GetCurrentRow() as EvaluationMetrics;

                if (data != null)
                {
                    XYDiagram diagram = chart.Diagram as XYDiagram;

                    if (diagram != null)
                    {
                        
                        diagram.AxisY.ConstantLines.Clear();

                        ConstantLine constantLine = new ConstantLine();
                        constantLine.AxisValue = data.StudentScore;
                        constantLine.LineStyle.DashStyle = DashStyle.Dash;
                        constantLine.Color = Color.FromArgb(149, 179, 215);
                        constantLine.LineStyle.Thickness = 1;

                        
                        constantLine.ShowInLegend = false;
                        constantLine.Title.Visible = false;

                        diagram.AxisY.ConstantLines.Add(constantLine);
                    }
                }
            }
        }
    }
}
