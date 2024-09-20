using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using NormalDistributionReport.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace NormalDistributionReport.Reports
{
    public partial class Chart2 : DevExpress.XtraReports.UI.XtraReport
    {
        public Chart2()
        {
            InitializeComponent();
        }

        private void xrChart1_BeforePrint(object sender, CancelEventArgs e)
        {
            XRChart chart = this.FindControl("xrChart1", true) as XRChart;

            if (chart != null)
            {
               
                var data = this.GetCurrentRow() as EvaluationMetrics;

                if (data != null)
                {
                    var report = this.Report.DataSource as List<NormalDistributionReportDTO>;
                    double rangeValue = data.RangeValue <= 0 ? 3 : data.RangeValue;

                    XYDiagram diagram = chart.Diagram as XYDiagram;

                    if (diagram != null)
                    {
                      
                        diagram.AxisX.ConstantLines.Clear();
                        diagram.AxisY.ConstantLines.Clear();

                        double zScore = data.StudentZScore;

                        // Garantir que o Z-Score esteja dentro do intervalo de -RangeValue a +RangeValue
                        zScore = Math.Max(-rangeValue, Math.Min(rangeValue, zScore));

                        // Criar a linha constante para a nota do aluno (Z-Score)
                        ConstantLine constantLine = new ConstantLine();
                        constantLine.AxisValue = zScore;
                        constantLine.LineStyle.DashStyle = DashStyle.Solid;
                        constantLine.Color = Color.FromArgb(149, 179, 215);
                        constantLine.LineStyle.Thickness = 1;
                        constantLine.ShowInLegend = false;
                        constantLine.Title.Visible = false;

                        diagram.AxisX.ConstantLines.Add(constantLine);

                        // Adicionar a linha de referência na posição 0 no eixo X
                        ConstantLine zeroLine = new ConstantLine();
                        zeroLine.AxisValue = 0;
                        zeroLine.LineStyle.DashStyle = DashStyle.Solid;
                        zeroLine.Color = Color.FromArgb(230, 230, 230);
                        zeroLine.LineStyle.Thickness = 1;
                        zeroLine.ShowInLegend = false;
                        zeroLine.Title.Visible = false;

                        diagram.AxisX.ConstantLines.Add(zeroLine);

                        // Adicionar um segundo eixo Y para o histograma
                        SecondaryAxisY axisY2 = new SecondaryAxisY("Secondary Y Axis");
                        diagram.SecondaryAxesY.Add(axisY2);

                        // Associar a Série 2 (histograma) ao eixo Y secundário
                        ((BarSeriesView)chart.Series[0].View).AxisY = axisY2;

                        // Definir o range do eixo X com base no RangeValue
                        diagram.AxisX.VisualRange.SetMinMaxValues(-rangeValue, rangeValue);
                        diagram.AxisX.WholeRange.SetMinMaxValues(-rangeValue, rangeValue);

                        // Verificar se o histograma deve ser exibido
                        if (data.ShowHistogram == true)
                        {
                            
                            chart.Series[0].Visible = true;
                        }
                        else
                        {
                           
                            chart.Series[0].Visible = false;
                        }

                        axisY2.Visibility = DevExpress.Utils.DefaultBoolean.False;
                    }
                }
            }
        }
    }
}
