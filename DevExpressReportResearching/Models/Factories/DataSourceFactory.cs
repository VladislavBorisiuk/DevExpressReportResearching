using DevExpressReportResearching.Models.Factories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevExpressReportResearching.Models.Factories
{
    public class DataSourceFactory
    {
        private readonly Dictionary<string, Lazy<IDataSourceFactory>> factories;

        public DataSourceFactory()
        {
            factories = new Dictionary<string, Lazy<IDataSourceFactory>>
        {
            { "EmployersDataSource", new Lazy<IDataSourceFactory>(() => new EmployersDataSourceFactory()) },
            { "StaffItemsDataSource", new Lazy<IDataSourceFactory>(() => new StaffItemDataSourceFactory()) }
        };
        }

        public object CreateDataSource(string name)
        {
            
            if (factories.TryGetValue(name, out var factory))
            {
                return factory.Value.CreateDataSource();
            }
            throw new ArgumentException($"Фабрика для типа {name} не найдена.");
        }
    }
}
