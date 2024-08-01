using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraReports.UI;
using DevExpressReportResearching.Models;
using DevExpressReportResearching.Models.Factories;
using DevExpressReportResearching.Services.Interfaces;
using XLabGlobal;


namespace DevExpressReportResearching.Services
{
    public class LoadReportService : ILoadReportService
    {
        private readonly DataSourceFactory dataSourceFactory;

        public LoadReportService()
        {
            dataSourceFactory = new DataSourceFactory();
            RegisterTrustedClasses();
        }
        private void RegisterTrustedClasses()
        {
            DevExpress.Utils.DeserializationSettings.RegisterTrustedClass(typeof(Employers));
            DevExpress.Utils.DeserializationSettings.RegisterTrustedClass(typeof(StaffItem));
        }

        public XtraReport LoadReport(string repxPath)
        {
            var report = new XtraReport();
            report.LoadLayoutFromXml(repxPath);

            var dataSources = report.ComponentStorage.OfType<ObjectDataSource>().ToList();

            foreach (var dataSource in dataSources)
            {
                var newDataSource = dataSourceFactory.CreateDataSource(dataSource.Name);
                dataSource.DataSource = newDataSource;
            }    
            return report;
        }
    }


}
