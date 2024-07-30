using DevExpress.CodeParser;
using DevExpress.XtraReports.UI;
using DevExpressReportResearching.Models;
using DevExpressReportResearching.Services.Interfaces;
using DevExpress.XtraReports.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using MessageBox = System.Windows.MessageBox;
using DevExpressReportResearching.Infrastructure.Reports;
using DevExpress.DataAccess.Sql;

namespace DevExpressReportResearching.Services
{
    public class DataService : IDataService
    {
        ReportDataContext _context;
        public DataService() 
        {
            _context = new ReportDataContext();
        }

        public List<string> CreateReport()
        {

            // Создание отчета вручную
            /*
            XtraReport report = new XtraReport();

            var data = GetData();
            if (data == null || !data.Any())
            {
                MessageBox.Show("Данные не найдены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            report.DataSource = data;
            report.DataMember = "";

            DetailBand detailBand = new DetailBand();
            
            XRTable table = new XRTable();
            table.BeginInit();
            table.WidthF = report.PageWidth - report.Margins.Left - report.Margins.Right;

            XRTableRow row = new XRTableRow();
            table.Rows.Add(row);

            XRTableCell cellID = new XRTableCell();
            cellID.DataBindings.Add(new XRBinding("Text", null, "ID"));
            cellID.WidthF = table.WidthF / 3;

            XRTableCell cellName = new XRTableCell();
            cellName.DataBindings.Add(new XRBinding("Text", null, "Name"));
            cellName.WidthF = table.WidthF / 3;

            XRTableCell cellPosition = new XRTableCell();
            cellPosition.DataBindings.Add(new XRBinding("Text", null, "Position"));
            cellPosition.WidthF = table.WidthF / 3;

            row.Cells.AddRange([cellID, cellName, cellPosition]);

            table.EndInit();

            detailBand.Controls.Add(table);
            report.Bands.Add(detailBand);

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Report files (*.repx)|*.repx|All files (*.*)|*.*",
                AddExtension = true,
                Title = "Save Report As"
            };


            var result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    string baseFileName = saveFileDialog.FileName;

                    report.SaveLayoutToXml(baseFileName);

                    string pdfFileName = System.IO.Path.ChangeExtension(baseFileName, ".pdf");
                    report.ExportToPdf(pdfFileName);

                    MessageBox.Show("Отчет успешно сохранен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }*/


            /*var report = new EmployersReport();
            //report.DataSource = GetData();
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Report files (*.repx)|*.repx|All files (*.*)|*.*",
                AddExtension = true,
                Title = "Save Report As"
            };

            var result = saveFileDialog.ShowDialog();

 
            if (result == DialogResult.OK) 
            {
                try
                {
                    string baseFileName = saveFileDialog.FileName;

                    report.SaveLayoutToXml(baseFileName);

                    string pdfFileName = System.IO.Path.ChangeExtension(baseFileName, ".pdf");
                    report.ExportToPdf(pdfFileName);

                    MessageBox.Show("Отчет успешно сохранен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }*/

            // Путь к папке, которую нужно проверить
            string folderPath = @"Resources"; // Замените на реальный путь
            string fileExtension = ".repx"; // Замените на нужное расширение

            try
            {
                // Проверка существования папки
                if (Directory.Exists(folderPath))
                {
                    // Получение всех файлов с указанным расширением
                    DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
                    FileInfo[] files = directoryInfo.GetFiles("*" + fileExtension);

                    // Вывод имен файлов
                    List<string> names = new();
                    foreach (FileInfo file in files)
                    {
                        names.Add(file.Name);
                    }
                    return names;
                }
                else
                {
                    Console.WriteLine($"Папка по пути '{folderPath}' не найдена.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
            return null;
        }

        public XtraReport? OpenReport()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Report files (*.repx)|*.repx|All files (*.*)|*.*",
                DefaultExt = "repx",
                AddExtension = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                XtraReport report = LoadReport(filePath);
                return report;
            }
            return null;
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

        public List<Employers> GetData()
        {
            return _context.Employers.ToList();
        }
    }
}
