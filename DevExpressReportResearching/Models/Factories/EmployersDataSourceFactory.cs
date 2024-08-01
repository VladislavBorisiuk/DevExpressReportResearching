using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.DataAccess.ObjectBinding;
using System;
using System.Collections.Generic;
using XLabGlobal;
using DevExpress.XtraReports.Native;
using DevExpressReportResearching.Models.Factories.Base;
using DevExpressReportResearching.Views.Windows;
using DevExpressReportResearching.ViewModels;

namespace DevExpressReportResearching.Models.Factories
{

    public class EmployersDataSourceFactory : IDataSourceFactory
    {
        public object CreateDataSource()
        {

            var employers = GetEmployers();
            return employers;
        }

        private List<Employers> GetEmployers()
        {
            var myWindow = App.Current.Windows
                .OfType<ChooseEmployersWindow>()
                .FirstOrDefault();
            var VM = myWindow.DataContext as ChooseEmployersViewModel;
            return VM.SelectedAvailableEmployers;
        }
    }

}
