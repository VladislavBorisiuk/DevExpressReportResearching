﻿using DevExpressReportResearching.Services;
using DevExpressReportResearching.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DevExpressReportResearching
{
    public partial class App
    {
        public static bool IsDesignMode { get; private set; } = true;
        public static Window ActivedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

        public static Window FocusedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);

        private static IHost _Host;

        public static IHost CurrentHost => _Host ??= new HostBuilder()
            .ConfigureAppConfiguration(cfg => cfg.AddJsonFile("appsettings.json", true, true))
            .ConfigureServices((hostContext, services) => services
                .AddViews()
                .AddServices()
            )
            .Build();

        public static IServiceProvider Services => CurrentHost.Services;

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = CurrentHost;
            base.OnStartup(e);
            await host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            using var host = CurrentHost;
            await host.StopAsync();
        }

        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            ViewModelRegistrator.AddViews(services);
            ServiceRegistrator.AddServices(services);
        }
        //


#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
        public static string CurrentDirectory => IsDesignMode ? Path.GetDirectoryName(GetSourceCodePath())
            : Environment.CurrentDirectory;
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.

        private static string GetSourceCodePath([CallerFilePath] string Path = null) => Path;
    }
}
