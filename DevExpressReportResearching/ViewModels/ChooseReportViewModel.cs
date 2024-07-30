using DevExpress.XtraReports.Serialization;
using DevExpress.XtraReports.UI;
using DevExpressReportResearching.Infrastructure.Commands;
using DevExpressReportResearching.Infrastructure.StaticObjects;
using DevExpressReportResearching.Models;
using DevExpressReportResearching.ViewModels.Base;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace DevExpressReportResearching.ViewModels
{
    class ChooseReportViewModel : ViewModel
    {
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
            string filePath = $"Resources/Templates/{RepxFileName}";
            XtraReport report = LoadReport(filePath);
            report.DataSource = EmpList;
            AppData.CurrentReport = report;
            App.ActivedWindow.Close();
        }

        private void Cancel()
        {
            App.ActivedWindow.Close();
        }
        public ChooseReportViewModel(List<Employers> list)
        {
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
            DevExpress.Utils.DeserializationSettings.RegisterTrustedClass(typeof(Employers));

            XtraReport report = new XtraReport();
            try
            {
                report.LoadLayoutFromXml(filePath);
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
