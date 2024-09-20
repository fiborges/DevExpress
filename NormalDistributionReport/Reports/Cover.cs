using DevExpress.XtraReports.UI;
using NormalDistributionReport.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace NormalDistributionReport.Reports
{
    public partial class Cover : DevExpress.XtraReports.UI.XtraReport
    {
        public Cover()
        {
            InitializeComponent();
        }

        private void xrPictureBox1_BeforePrint(object sender, CancelEventArgs e)
        {
            Console.WriteLine("before print imagem do cover");
            var data = GetCurrentRow() as NormalDistributionReportDTO;
            if (data != null && data.CoverImage != null)
            {
                using (MemoryStream ms = new MemoryStream(data.CoverImage))
                {
                    xrPictureBox1.Image = Image.FromStream(ms);
                }
            }
            else
            {
                Console.WriteLine("A imagem da capa não foi encontrada ou está vazia.");
            }
        }
    }
}
