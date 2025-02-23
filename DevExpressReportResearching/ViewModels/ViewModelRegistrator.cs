﻿using Microsoft.Extensions.DependencyInjection;

namespace DevExpressReportResearching.ViewModels
{
    internal static class ViewModelRegistrator
    {
        public static IServiceCollection AddViews(this IServiceCollection services) => services
           .AddTransient<MainWindowViewModel>()
           .AddTransient<ChooseEmployersViewModel>()
           .AddTransient<ChooseReportViewModel>() 
        ;
    }
}