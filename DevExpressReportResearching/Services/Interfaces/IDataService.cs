using DevExpress.XtraReports.UI;
using DevExpressReportResearching.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace DevExpressReportResearching.Services.Interfaces
{
    internal interface IDataService
    {
        XtraReport? OpenReport();
        List<Employers> GetData();
        List<string> CreateReport();
    }
}
