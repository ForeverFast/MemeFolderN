using MemeFolderN.MFViewModels.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MemeFolderN.MFViews
{
    public partial class MFWindow : Window
    {
        private IServiceProvider _serviceProvider;

        public ContentControl FrameContentControl => MFContent; 

        public MFWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = _serviceProvider.GetService(typeof(MFViewModel));
           
            ((MFViewModel)DataContext).FolderLoadCommand.Execute(null);
            ((MFViewModel)DataContext).MemeLoadCommand.Execute(null);
            ((MFViewModel)DataContext).MemeTagLoadCommand.Execute(null);
        }
    }
}
