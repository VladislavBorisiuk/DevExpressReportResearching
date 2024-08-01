using DevExpressReportResearching.Models;
using DevExpressReportResearching.Services.Interfaces;


namespace DevExpressReportResearching.Services
{
    public class DataService : IDataService
    {
        ReportDataContext _context;
        public DataService() 
        {
            _context = new ReportDataContext();
        }

        public List<Employers> GetData()
        {
            return _context.Employers.ToList();
        }
    }
}
