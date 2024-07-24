using DevExpressREportGeneretor.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DevExpressREportGeneretor.Services
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
           .AddTransient<IDataService, DataService>()
           .AddTransient<IUserDialog, UserDialog>()
        ;
    }
}
