using MemeFolderN.EntityFramework;
using MemeFolderN.EntityFramework.Services;
using MemeFolderN.Extentions.Services;
using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFModelBase.Default;
using MemeFolderN.MFViewModels.Default;
using MemeFolderN.MFViewModels.Default.Extentions;
using MemeFolderN.MFViewModels.Default.MethodCommands;
using MemeFolderN.MFViewModels.Default.Services;
using MemeFolderN.MFViewModelsBase.Services;
using MemeFolderN.MFViews;
using MemeFolderN.MFViews.Pages;
using MemeFolderN.Navigation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

                #region NavManager

                MFWindow mainWindow = ServiceProvider.GetRequiredService<MFWindow>();
                INavigationService navigationService = ServiceProvider.GetRequiredService<INavigationService>();

                navigationService.RegisterViewType<FolderUC>("folderPage");

                navigationService.Register<StartUC>("root", null);
                //navigationService.Register<SettingsPage>("settings", ServiceProvider.GetRequiredService<SettingsPageVM>());
                //navigationService.Register<SearchPage>("searchPage", ServiceProvider.GetRequiredService<SearchPageVM>());
                navigationService.Navigate("root", NavigationType.Root);

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
            services.AddSingleton(typeof(ShowDialogDelegete), (ShowDialogDelegete)((vm, id) => MaterialDesignThemes.Wpf.DialogHost.Show(vm, id)));

            services.AddSingleton<IMemeDataService, MemeDataService>();
            services.AddSingleton<IFolderDataService, FolderDataService>();
            services.AddSingleton<IMemeTagDataService, MemeTagDataService>();
            services.AddSingleton<IMemeTagNodeDataService, MemeTagNodeDataService>();
            services.AddSingleton(typeof(MemeFolderNDbContextFactory));

            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IUserSettingsService, UserSettingsService>();
            services.AddSingleton<INavigationService, NavigationService>();


            services.AddSingleton<IFolderMethodCommandsClass, FolderMethodCommandsClass>();
            services.AddSingleton<IMemeMethodCommandsClass, MemeMethodCommandsClass>();
            services.AddSingleton<IMemeTagMethodCommandsClass, MemeTagMethodCommandsClass>();
            services.AddSingleton<INavCommandsClass, NavCommandsClass>();

            services.AddSingleton(typeof(VmDIContainer));
            services.AddSingleton(service => Configuration);

            services.AddSingleton(typeof(Dispatcher), Current.Dispatcher);
            //services.AddSingleton(typeof(SearchPageVM));
            //services.AddSingleton(typeof(SettingsPageVM));
            services.AddSingleton<IMFModel, MFModel>();
            services.AddSingleton(typeof(MFViewModel));
            services.AddSingleton(typeof(MFWindow));



            services.AddSingleton(typeof(ContentControl), (s) => s.GetRequiredService<MFWindow>().FrameContentControl);

        }
    }
}
