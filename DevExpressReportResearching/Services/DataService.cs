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

        public void CreateReport()
        {

            // Создание отчета вручную
            /*
            XtraReport report = new XtraReport();

            // Получаем данные и проверяем их наличие
            var data = GetData();
            if (data == null || !data.Any())
            {
                MessageBox.Show("Данные не найдены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Настройка источника данных
            report.DataSource = data;
            report.DataMember = "";

            // Создание детали
            DetailBand detailBand = new DetailBand();
            report.Bands.Add(detailBand);

            // Создание таблицы
            XRTable table = new XRTable();
            table.BeginInit();
            table.WidthF = report.PageWidth - report.Margins.Left - report.Margins.Right;

            XRTableRow row = new XRTableRow();
            table.Rows.Add(row);

            // Создание и настройка ячеек
            XRTableCell cellID = new XRTableCell();
            cellID.DataBindings.Add(new XRBinding("Text", null, "ID"));
            cellID.WidthF = table.WidthF / 3;

            XRTableCell cellName = new XRTableCell();
            cellName.DataBindings.Add(new XRBinding("Text", null, "Name"));
            cellName.WidthF = table.WidthF / 3;

            XRTableCell cellPosition = new XRTableCell();
            cellPosition.DataBindings.Add(new XRBinding("Text", null, "Position"));
            cellPosition.WidthF = table.WidthF / 3;

            // Добавление ячеек в строку
            row.Cells.AddRange([cellID, cellName, cellPosition]);

            table.EndInit();

            // Добавление таблицы в полосу деталей
            detailBand.Controls.Add(table);

            // Настройка диалога сохранения файла
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Report files (*.repx)|*.repx|All files (*.*)|*.*",
                AddExtension = true,
                Title = "Save Report As"
            };

            // Показываем диалог
            var result = saveFileDialog.ShowDialog();

            // Проверяем, был ли выбран файл
            if (result == DialogResult.OK)
            {
                try
                {
                    // Получаем путь для сохранения
                    string baseFileName = saveFileDialog.FileName;

                    // Сохраняем отчет в выбранный файл
                    report.SaveLayoutToXml(baseFileName);

                    // Экспортируем отчет в PDF с добавлением расширения
                    string pdfFileName = System.IO.Path.ChangeExtension(baseFileName, ".pdf");
                    report.ExportToPdf(pdfFileName);

                    MessageBox.Show("Отчет успешно сохранен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    // Обработка исключений
                    MessageBox.Show($"Ошибка сохранения отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }*/


            var report = new EmployersReport();
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
            }


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
