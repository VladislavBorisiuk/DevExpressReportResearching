using Microsoft.Extensions.DependencyInjection;

namespace DevExpressReportResearching.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
