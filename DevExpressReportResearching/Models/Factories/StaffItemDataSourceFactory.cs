using DevExpressReportResearching.Models.Factories.Base;
using XLabGlobal;

namespace DevExpressReportResearching.Models.Factories
{
    internal class StaffItemDataSourceFactory : IDataSourceFactory
    {
        public object CreateDataSource()
        {
            
            var staffItems = GetStaffItemsFromOData();
            return staffItems;
        }

        private List<StaffItem> GetStaffItemsFromOData()
        {
            // Здесь код для получения StaffItems через OData
            return new List<StaffItem>
            {
                new StaffItem { Email = "Vlad@gmail", Phone="12345123", FullName="Vladislav" },
                new StaffItem { Email = "Sobaka@gmail", Phone="333333333", FullName="Sobaken" }
            };
        }
    }
}
