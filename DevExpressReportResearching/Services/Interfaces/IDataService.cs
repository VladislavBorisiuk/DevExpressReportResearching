using DevExpress.XtraReports.UI;
using DevExpressReportResearching.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace DevExpressReportResearching.Services.Interfaces
{
    internal interface IDataService
    {
        List<Employers> GetData();
    }
}
