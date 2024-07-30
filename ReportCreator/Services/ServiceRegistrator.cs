using Microsoft.Extensions.DependencyInjection;
using ReportCreator.Services.Interfaces;

namespace ReportCreator.Services
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
           .AddTransient<IDataService, DataService>()
           .AddTransient<IUserDialog, UserDialog>()
        ;
    }
}
