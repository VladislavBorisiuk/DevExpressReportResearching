using Microsoft.Extensions.DependencyInjection;

namespace DevExpressREportGeneretor.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
