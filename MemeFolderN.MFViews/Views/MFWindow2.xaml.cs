using MemeFolderN.MFViewModels.Default;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MemeFolderN.Views
{
    /// <summary>
    /// Логика взаимодействия для MFWindow.xaml
    /// </summary>
    public partial class MFWindow : Window
    {
        private IServiceProvider _serviceProvider;

        //public event RoutedEventHandler DataContextLoaded;

        public MFWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = _serviceProvider.GetService(typeof(MFViewModel));
            //DataContextLoaded?.Invoke(this, null);
            ((MFViewModel)DataContext).FolderRootsCommand.Execute(null);
            ((MFViewModel)DataContext).MemeTagLoadCommand.Execute(null);
        }

        private void empListBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer sv = (ScrollViewer)sender;
            double offset = sv.ContentHorizontalOffset + (e.Delta / 120);
            sv.ScrollToHorizontalOffset(offset);
        }
    }
}
