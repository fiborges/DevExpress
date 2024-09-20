using CandidateReportGenerator.Models;
using CandidateReportGenerator.Reports;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System;

namespace CandidateReportGenerator.Controllers
{
    public class ReportController
    {
        public XtraReport CreateCandidateReport(List<CandidateReportDTO> candidateReports, List<int> selectedPhases, string language = "en-EN")
        {
            var report = new CandidateReport();

            report.DataSource = candidateReports;

            // título do relatório
            var titleLabel = report.FindControl("xrLabel1", true) as XRLabel;
            if (titleLabel != null)
            {
                titleLabel.Text = language == "pt-PT" ? "Resultados no Procedimento de Avaliação" : "Results in the Evaluation Procedure";
            }

            //  logo 
            var logoPictureBox = report.FindControl("xrPictureBox1", true) as XRPictureBox;
            if (logoPictureBox != null && candidateReports[0].CompanyLogo != null)
            {
                using (var ms = new MemoryStream(candidateReports[0].CompanyLogo))
                {
                    logoPictureBox.Image = Image.FromStream(ms);
                }
            }

            float currentYPosition = 70;

            
            string candidateNameLabel = language == "pt-PT" ? "Nome: " : "Name: ";
            string candidateEmailLabel = language == "pt-PT" ? "Email: " : "Email: ";
            string candidateIdLabel = language == "pt-PT" ? "ID do Candidato: " : "Candidate ID: ";

            float labelWidth = 100f; 
            float valueXPosition = labelWidth + 5f;

            XRLabel nameLabel = new XRLabel
            {
                Text = candidateNameLabel,
                Font = new DevExpress.Drawing.DXFont("Arial", 10, DevExpress.Drawing.DXFontStyle.Bold),
                LocationF = new DevExpress.Utils.PointFloat(0, currentYPosition),
                WidthF = labelWidth,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            };
            report.Bands[BandKind.Detail].Controls.Add(nameLabel);

            XRLabel nameValueLabel = new XRLabel
            {
                Text = candidateReports[0].CandidateName,
                Font = new DevExpress.Drawing.DXFont("Arial", 10, DevExpress.Drawing.DXFontStyle.Regular),
                LocationF = new DevExpress.Utils.PointFloat(valueXPosition, currentYPosition),
                WidthF = report.PageWidth - report.Margins.Left - report.Margins.Right - valueXPosition,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            };
            report.Bands[BandKind.Detail].Controls.Add(nameValueLabel);

            currentYPosition += nameLabel.HeightF + 5;

            XRLabel emailLabel = new XRLabel
            {
                Text = candidateEmailLabel,
                Font = new DevExpress.Drawing.DXFont("Arial", 10, DevExpress.Drawing.DXFontStyle.Bold),
                LocationF = new DevExpress.Utils.PointFloat(0, currentYPosition),
                WidthF = labelWidth,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            };
            report.Bands[BandKind.Detail].Controls.Add(emailLabel);

            XRLabel emailValueLabel = new XRLabel
            {
                Text = candidateReports[0].CandidateID,
                Font = new DevExpress.Drawing.DXFont("Arial", 10, DevExpress.Drawing.DXFontStyle.Regular),
                LocationF = new DevExpress.Utils.PointFloat(valueXPosition, currentYPosition),
                WidthF = report.PageWidth - report.Margins.Left - report.Margins.Right - valueXPosition,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            };
            report.Bands[BandKind.Detail].Controls.Add(emailValueLabel);

            currentYPosition += emailLabel.HeightF + 5;

            XRLabel idLabel = new XRLabel
            {
                Text = candidateIdLabel,
                Font = new DevExpress.Drawing.DXFont("Arial", 10, DevExpress.Drawing.DXFontStyle.Bold),
                LocationF = new DevExpress.Utils.PointFloat(0, currentYPosition),
                WidthF = labelWidth,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            };
            report.Bands[BandKind.Detail].Controls.Add(idLabel);

            XRLabel idValueLabel = new XRLabel
            {
                Text = candidateReports[0].CandidateID,
                Font = new DevExpress.Drawing.DXFont("Arial", 10, DevExpress.Drawing.DXFontStyle.Regular),
                LocationF = new DevExpress.Utils.PointFloat(valueXPosition, currentYPosition),
                WidthF = report.PageWidth - report.Margins.Left - report.Margins.Right - valueXPosition,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            };
            report.Bands[BandKind.Detail].Controls.Add(idValueLabel);

            currentYPosition += idLabel.HeightF + 70;

            
            float tableWidth = report.PageWidth - report.Margins.Left - report.Margins.Right;
            float firstColumnWidth = tableWidth * 0.3f;
            float secondColumnWidth = tableWidth * 0.2f;
            float thirdColumnWidth = tableWidth * 0.5f;

            foreach (var phase in candidateReports[0].PhaseScores)
            {
                if (selectedPhases.Contains(phase.PhaseId))
                {
                    XRTable table = new XRTable();
                    table.WidthF = tableWidth;
                    table.LocationF = new DevExpress.Utils.PointFloat(0, currentYPosition);

                    XRTableRow headerRow = new XRTableRow();
                    XRTableCell headerCell = new XRTableCell
                    {
                        Text = phase.PhaseName,
                        Font = new DevExpress.Drawing.DXFont("Arial", 10, DevExpress.Drawing.DXFontStyle.Bold),
                        Borders = DevExpress.XtraPrinting.BorderSide.Bottom,
                        WidthF = table.WidthF
                    };
                    headerRow.Cells.Add(headerCell);
                    table.Rows.Add(headerRow);

                    foreach (var metric in phase.Metrics)
                    {
                        XRTableRow row = new XRTableRow();
                        row.Cells.Add(new XRTableCell { Text = metric.Name, WidthF = firstColumnWidth });
                        row.Cells.Add(new XRTableCell { Text = metric.Score.ToString(), WidthF = secondColumnWidth });
                        row.Cells.Add(new XRTableCell { Text = metric.Description, WidthF = thirdColumnWidth });
                        table.Rows.Add(row);
                    }

                    report.Bands[BandKind.Detail].Controls.Add(table);

                    currentYPosition += table.HeightF + 100;
                }
            }

            if (candidateReports[0].ShowFinalScore)
            {
                currentYPosition += 40;
                string finalScoreText = language == "pt-PT" ? $"Nota Final: {candidateReports[0].FinalScore}" : $"Final Score: {candidateReports[0].FinalScore}";

                XRLabel finalScoreLabel = new XRLabel
                {
                    Text = finalScoreText,
                    Font = new DevExpress.Drawing.DXFont("Arial", 10, DevExpress.Drawing.DXFontStyle.Bold),
                    LocationF = new DevExpress.Utils.PointFloat(0, currentYPosition),
                    WidthF = report.PageWidth - report.Margins.Left - report.Margins.Right,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
                };

                report.Bands[BandKind.Detail].Controls.Add(finalScoreLabel);
            }

            var pageFooterBand = new PageFooterBand
            {
                HeightF = 30
            };

            string footerText = language == "pt-PT" ? "Relatório gerado em: " : "Report generated on: ";
            footerText += DateTime.Now.ToString("dd-MM-yyyy");

            XRLabel footerLabel = new XRLabel
            {
                Text = footerText,
                Font = new DevExpress.Drawing.DXFont("Arial", 9, DevExpress.Drawing.DXFontStyle.Regular),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                LocationF = new DevExpress.Utils.PointFloat(0, 0),
                WidthF = report.PageWidth - report.Margins.Left - report.Margins.Right
            };
            pageFooterBand.Controls.Add(footerLabel);

            XRPageInfo pageInfo = new XRPageInfo
            {
                PageInfo = DevExpress.XtraPrinting.PageInfo.NumberOfTotal,
                Font = new DevExpress.Drawing.DXFont("Arial", 9, DevExpress.Drawing.DXFontStyle.Regular),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight,
                LocationF = new DevExpress.Utils.PointFloat(report.PageWidth - report.Margins.Left - report.Margins.Right - 100, 0),
                WidthF = 100
            };
            pageFooterBand.Controls.Add(pageInfo);

            report.Bands.Add(pageFooterBand);
            pageFooterBand.PrintOn = DevExpress.XtraReports.UI.PrintOnPages.NotWithReportHeader;

            return report;
        }
    }
}
