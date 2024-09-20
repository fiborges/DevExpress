using DevExpress.XtraReports.UI;
using System.ComponentModel;
using DevExpress.DataAccess.ObjectBinding;

namespace NormalDistributionReport.Reports
{
    public partial class NormalDistributionReport
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new ObjectDataSource DataSource
        {
            get
            {
                var objectDataSource = new ObjectDataSource(this.components);
                objectDataSource.DataSource = typeof(global::NormalDistributionReport.Models.NormalDistributionReportDTO);
                return objectDataSource;
            }
            set { base.DataSource = value; }
        
        }
    }
}