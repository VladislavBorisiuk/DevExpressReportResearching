using DevExpress.Data.Browsing;
using DevExpressReportResearching.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevExpressReportResearching
{
    internal class ReportDataContext : DbContext
    {
        public DbSet<Employers> Employers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Создание пути к файлу базы данных
            var databasePath = Path.Combine(appDirectory, "Resources/Data/ReportData.db");

            // Создание строки подключения
            var connectionString = $"Data Source={databasePath}";

            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
