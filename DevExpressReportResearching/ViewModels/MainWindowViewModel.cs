using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Reports.UserDesigner;
using DevExpress.XtraReports.UI;
using DevExpressReportResearching.Infrastructure.Commands;
using DevExpressReportResearching.Infrastructure.Reports;
using DevExpressReportResearching.Models;
using DevExpressReportResearching.Services;
using DevExpressReportResearching.Services.Interfaces;
using DevExpressReportResearching.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DevExpressReportResearching.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IDataService _dataService;
        private XtraReport report;
        public XtraReport Report
        {
            get => report; 
            set => Set(ref report, value);
        }
        public ICommand SaveReportCommand { get; }
        public ICommand OpenReportCommand { get; }

        private List<Employers> _employees;
        
        public List<Employers> Employers
        {
            get => _employees;

            set => Set(ref _employees , value);
        }
        public MainWindowViewModel(IDataService dataservice)
        {
            _dataService = dataservice;
            SaveReportCommand = new RelayCommand(SaveReport);
            OpenReportCommand = new RelayCommand(OpenReport);
            Employers = _dataService.GetData();
            Report = new EmployersReport();
        }

        private void SaveReport()
        {
            _dataService.CreateReport();
        }

        private void OpenReport() 
        {
            Report = _dataService.OpenReport();
        }
    }
}
