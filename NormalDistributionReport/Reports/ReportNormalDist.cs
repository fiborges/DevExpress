using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using NormalDistributionReport.Models;
using NormalDistributionReport.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace NormalDistributionReport.Reports
{
    public partial class ReportNormalDist : DevExpress.XtraReports.UI.XtraReport
    {
        public ReportNormalDist()
        {
            InitializeComponent();
        }

        private void xrChart1_BeforePrint(object sender, CancelEventArgs e)
        {
            XRChart chart = this.FindControl("xrChart1", true) as XRChart;

            if (chart != null)
            {
                var report = this.Report.DataSource as List<NormalDistributionReportDTO>;
                var reportData = report?.FirstOrDefault();

                if (reportData != null)
                {
                    double studentZScore = reportData.StudentZScore;
                    double rangeValue = reportData.RangeValue;
                    //Console.WriteLine($"Student Z-Score: {studentZScore}, Range Value: {rangeValue}");

                    XYDiagram diagram = chart.Diagram as XYDiagram;

                    if (diagram != null)
                    {
                        // Configurar o eixo X dinamicamente com o rangeValue
                        diagram.AxisX.VisualRange.SetMinMaxValues(-rangeValue, rangeValue);
                        diagram.AxisX.WholeRange.SetMinMaxValues(-rangeValue, rangeValue);

                        diagram.AxisX.ConstantLines.Clear();
                        diagram.AxisY.ConstantLines.Clear();

                        // Garantir que o Z-Score esteja dentro do intervalo dinâmico de -RangeValue a +RangeValue
                        studentZScore = Math.Max(-rangeValue, Math.Min(rangeValue, studentZScore));
                        //Console.WriteLine($"Z-Score (clamped) grafico geral: {studentZScore}");

                        // Criar a linha constante para a nota final do aluno
                        ConstantLine constantLine = new ConstantLine();
                        constantLine.AxisValue = studentZScore;
                        constantLine.LineStyle.DashStyle = DashStyle.Solid;
                        constantLine.Color = Color.FromArgb(149, 179, 215);
                        constantLine.LineStyle.Thickness = 1;
                        constantLine.ShowInLegend = false;
                        constantLine.Title.Visible = false;
                        diagram.AxisX.ConstantLines.Add(constantLine);

                        // debug
                        //Console.WriteLine($"Linha azul (Z-Score) posição X: {constantLine.AxisValue}");

                        // Adicionar uma linha de referência na posição 0
                        ConstantLine zeroLine = new ConstantLine();
                        zeroLine.AxisValue = 0;
                        zeroLine.LineStyle.DashStyle = DashStyle.Solid;
                        zeroLine.Color = Color.FromArgb(230, 230, 230);
                        zeroLine.LineStyle.Thickness = 1;
                        zeroLine.ShowInLegend = false;
                        zeroLine.Title.Visible = false;
                        diagram.AxisX.ConstantLines.Add(zeroLine);

                        // Adicionar um segundo eixo Y para a Série 1 (histograma)
                        SecondaryAxisY axisY2 = new SecondaryAxisY("Secondary Y Axis");
                        diagram.SecondaryAxesY.Add(axisY2);
                        ((BarSeriesView)chart.Series[0].View).AxisY = axisY2;
                        axisY2.Visibility = DevExpress.Utils.DefaultBoolean.False;

                        if (reportData.ShowHistogram)
                        {
                           
                            chart.Series[0].Visible = true;
                        }
                        else
                        {
                            
                            chart.Series[0].Visible = false;
                        }
                    }
                }
            }
        }








    }
}
