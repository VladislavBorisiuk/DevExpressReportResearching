using DevExpress.XtraReports.UI;
using DevExpressREportGeneretor.Infrastructure.Commands;
using DevExpressREportGeneretor.Services.Interfaces;
using DevExpressREportGeneretor.ViewModels.Base;
using DevExpressReportResearching.Models;
using System.Windows.Input;

namespace DevExpressREportGeneretor.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private XtraReport _reportDocument;
        public XtraReport ReportDocument
        {
            get => _reportDocument;
            set => Set(ref _reportDocument, value);
        }

        public ICommand GenerateReportCommand { get; }
        public ICommand CloseCommand { get; }

        public MainWindowViewModel()
        {
            GenerateReportCommand = new RelayCommand(GenerateReport);
            CloseCommand = new RelayCommand(Close);
        }

        private void GenerateReport()
        {
            var saveFileDialog = new OpenFileDialog
            {
                Filter = "Report files (*.repx)|*.repx|All files (*.*)|*.*",
                DefaultExt = "repx",
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Загрузка отчета из файла
                var report = new XtraReport();
                report.LoadLayout(saveFileDialog.FileName);

                // Установка источника данных
                report.DataSource = GetData();

                ReportDocument = report;
            }
            
        }

        private IEnumerable<Employee> GetData()
        {
            return new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Position = "Manager" },
                new Employee { Id = 2, Name = "Jane Smith", Position = "Developer" },
                new Employee { Id = 3, Name = "Sam Brown", Position = "Designer" }
            };
        }

        private void Close()
        {
           
        }
    }
}
