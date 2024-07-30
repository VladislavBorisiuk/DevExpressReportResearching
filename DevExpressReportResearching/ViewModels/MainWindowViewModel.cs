using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Reports.UserDesigner;
using DevExpress.XtraReports.UI;
using DevExpressReportResearching.Infrastructure.Commands;
using DevExpressReportResearching.Infrastructure.Reports;
using DevExpressReportResearching.Infrastructure.StaticObjects;
using DevExpressReportResearching.Models;
using DevExpressReportResearching.Services;
using DevExpressReportResearching.Services.Interfaces;
using DevExpressReportResearching.ViewModels.Base;
using DevExpressReportResearching.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DevExpressReportResearching.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IDataService _dataService;
        private readonly IUserDialog _userDialog;
        
        public XtraReport Report
        {
            get => AppData.CurrentReport; 
            set => Set(ref AppData.CurrentReport, value);
        }
        public ICommand SaveReportCommand { get; }
        public ICommand OpenReportCommand { get; }

        private List<Employers> _employees;
        
        public List<Employers> Employers
        {
            get => _employees;

            set => Set(ref _employees , value);
        }

        private List<Employers> _selectedEmployees;

        public List<Employers> SelectedEmployers
        {
            get => _selectedEmployees;

            set => Set(ref _selectedEmployees, value);
        }
        public MainWindowViewModel(IDataService dataservice, IUserDialog dialog)
        {
            _dataService = dataservice;
            _userDialog = dialog;
            SaveReportCommand = new RelayCommand(SaveReport);
            OpenReportCommand = new RelayCommand(OpenReport);
            Employers = _dataService.GetData();
            SelectedEmployers = new List<Employers>();
            Report = new EmployersReport()
            {
                DataSource = Employers
            };
        }

        private void SaveReport()
        {
           _dataService.CreateReport();
        }
        
        private void OpenReport() 
        {
            ChooseEmployersWindow chooseEmployersWindow = new ChooseEmployersWindow();
            chooseEmployersWindow.DataContext = new ChooseEmployersViewModel(Employers);
            chooseEmployersWindow.Owner = App.Current.MainWindow;
            chooseEmployersWindow.ShowDialog();
            OnPropertyChanged(nameof(Report));
        }
    }
}
