using Microsoft.Extensions.DependencyInjection;

namespace DevExpressReportResearching.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();

        public ChooseEmployersViewModel CEmployersWindowModel => App.Services.GetRequiredService<ChooseEmployersViewModel>();

        public ChooseReportViewModel CReportWindowModel => App.Services.GetRequiredService<ChooseReportViewModel>();
    }
}
