using DevExpress.XtraReports.UI;

namespace DevExpressReportResearching.Services.Interfaces
{
    public interface ILoadReportService
    {
        public XtraReport LoadReport(string repxPath);
    }
}
