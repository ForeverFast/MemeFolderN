using AutoMapper;
using MemeFolderN.Data;
using MemeFolderN.Data.Services;
using MemeFolderN.MFModel.Wpf.Abstractions;
using MemeFolderN.MFModel.Wpf;
using MemeFolderN.MFViewModels.Wpf;
using MemeFolderN.MFViewModels.Wpf.Extentions;
using MemeFolderN.MFViewModels.Wpf.Services;
using MemeFolderN.MFViews.Wpf;
using MemeFolderN.MFViews.Wpf.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvvmNavigation;
using MvvmNavigation.Abstractions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Z.EntityFramework.Extensions;
using MemeFolderN.MFViewModels.Common.Services;
using MemeFolderN.Common.Managers;

namespace MemeFolderN
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }
       

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //CurrentUser = new User() { Login = "ForeverFast" };

            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("sxo-settings.json", optional: false, reloadOnChange: true);

            //Configuration = builder.Build();

            try
            {
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                ServiceProvider = serviceCollection.BuildServiceProvider();

                EntityFrameworkManager.ContextFactory = context => ServiceProvider.GetRequiredService<MemeFolderNDbContextFactory>().CreateDbContext(null);

                #region NavManager

                MFWindow mainWindow = ServiceProvider.GetRequiredService<MFWindow>();
                INavigationManager navigationManager = ServiceProvider.GetRequiredService<INavigationManager>();

                navigationManager.RegisterViewType<FolderUC>("folderPage");
                navigationManager.RegisterViewType<MemeTagUC>("memeTagPage");

                navigationManager.Register<StartUC>("root", null);
                //navigationService.Register<SettingsPage>("settings", ServiceProvider.GetRequiredService<SettingsPageVM>());
                //navigationService.Register<SearchPage>("searchPage", ServiceProvider.GetRequiredService<SearchPageVM>());
                navigationManager.Navigate("root", NavigationType.Root);

                #endregion

                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.StackTrace}");
            }

        }


        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(AutoMapperConfigurationFactory));
            services.AddSingleton(typeof(MapperConfiguration), s => s.GetRequiredService<AutoMapperConfigurationFactory>().GetMapperConfiguration());
            services.AddSingleton(typeof(Mapper));

            services.AddSingleton<IMemeDataService, MemeDataService>();
            services.AddSingleton<IFolderDataService, FolderDataService>();
            services.AddSingleton<IMemeTagDataService, MemeTagDataService>();
            services.AddSingleton<IMemeTagNodeDataService, MemeTagNodeDataService>();
            services.AddSingleton<IExtentionalDataService, ExtentionalDataService>();
            services.AddSingleton(typeof(MemeFolderNDbContextFactory));

            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<UserSettingsManager>();
            services.AddSingleton<INavigationManager, NavigationManager>();

            services.AddSingleton(typeof(VmDIContainer));
            services.AddSingleton(service => Configuration);

            services.AddSingleton(typeof(Dispatcher), Current.Dispatcher);
            //services.AddSingleton(typeof(SearchPageVM));
            //services.AddSingleton(typeof(SettingsPageVM));
            services.AddSingleton<IMFModelWpf, MFModelWpf>();
            services.AddSingleton(typeof(MFViewModel));
            services.AddSingleton(typeof(MFWindow));

            services.AddSingleton(typeof(ShowDialogDelegete), (ShowDialogDelegete)((vm, id) => MaterialDesignThemes.Wpf.DialogHost.Show(vm, id)));
            services.AddSingleton(typeof(ContentControl), (s) => s.GetRequiredService<MFWindow>().FrameContentControl);
        }
    }
}
