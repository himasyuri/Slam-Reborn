using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SlamReborn.Core;
using SlamReborn.Core.SourceEngine;
using SlamReborn.Models;
using SlamReborn.Stores;
using SlamReborn.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SlamReborn
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<Cfg>();
                    services.AddTransient<Initializer>();
                    services.AddTransient<ITrackController, TrackController>();
                    services.AddTransient<IFileDialogService, FileDialogService>();
                    services.AddTransient<Logger>();
                    services.AddTransient<Converter>();
                    services.AddTransient<YTDownloader>();

                    services.AddSingleton<ModalNavigationStore>();
                    services.AddSingleton<SelectedGameStore>();
                    services.AddSingleton<GamesStore>();
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<TracksStore>();

                    services.AddTransient<StartViewModel>(CreateStartViewModel);
                    services.AddSingleton<MainWindow>((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }

        private StartViewModel CreateStartViewModel(IServiceProvider services)
        {
            return StartViewModel.LoadViewModel(
                services.GetRequiredService<GamesStore>(),
                services.GetRequiredService<SelectedGameStore>(),
                services.GetRequiredService<ModalNavigationStore>(),
                services.GetRequiredService<ITrackController>(),
                services.GetRequiredService<IFileDialogService>(),
                services.GetRequiredService<YTDownloader>(),
                services.GetRequiredService<TracksStore>());
        }
    }
}
