using MemeFolderN.DevTools.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MemeFolderN.DevTools.Views
{
    /// <summary>
    /// Логика взаимодействия для DevBoard.xaml
    /// </summary>
    public partial class DevBoard : UserControl
    {
        public DevBoard()
        {
            InitializeComponent();
            DataContext = new DevViewModel();
        }
    }
}
