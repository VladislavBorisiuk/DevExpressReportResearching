using DevExpress.DataAccess.Native.Sql.MasterDetail;
using DevExpress.XtraReports.Serialization;
using DevExpress.XtraReports.UI;
using DevExpressReportResearching.Infrastructure.Commands;
using DevExpressReportResearching.Infrastructure.StaticObjects;
using DevExpressReportResearching.Models;
using DevExpressReportResearching.Services;
using DevExpressReportResearching.Services.Interfaces;
using DevExpressReportResearching.ViewModels.Base;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using XLabGlobal;
using MessageBox = System.Windows.MessageBox;

namespace DevExpressReportResearching.ViewModels
{
    class ChooseReportViewModel : ViewModel
    {
        private LoadReportService _loadReportService;
        private XtraReport report;
        private List<Employers> EmpList;
        private ObservableCollection<string> _repxFiles;
        public ObservableCollection<string> RepxFiles
        {
            get => _repxFiles;
            set => Set(ref _repxFiles, value);
        }

        private string _repxFileName;

        public string RepxFileName
        {
            get => _repxFileName;

            set => Set(ref _repxFileName, value);
        }

        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }

        private void Confirm()
        {
            if(!string.IsNullOrEmpty(RepxFileName))
            {
                string filePath = $"Resources/Templates/{RepxFileName}";
                XtraReport report = LoadReport(filePath);
                AppData.CurrentReport = report;
                App.ActivedWindow.Close();
            }
            else
            {
                MessageBox.Show("Выбирите шаблон отчета","Warning");
            }
        }

        private void Cancel()
        {
            App.ActivedWindow.Close();
        }
        public ChooseReportViewModel(List<Employers> list)
        {
            _loadReportService = new LoadReportService();
            EmpList = list;
            LoadRepxFiles(Directory.GetCurrentDirectory()+"/Resources/Templates"); 
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void LoadRepxFiles(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                RepxFiles = new ObservableCollection<string>(
                    Directory.GetFiles(directoryPath, "*.repx").Select(Path.GetFileName));
            }
        }

        private XtraReport LoadReport(string filePath)
        {

            try
            {
                report = _loadReportService.LoadReport(filePath);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка загрузки отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return report;
        }
    }
}
