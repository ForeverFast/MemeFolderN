using MaterialDesignThemes.Wpf;
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

namespace MemeFolderN.MFViews.UserControlles.MFButtonUC
{
    /// <summary>
    /// Логика взаимодействия для MFButton.xaml
    /// </summary>
    public partial class MFButton : Button
    {
        public MFButton()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IconKindProperty =
            DependencyProperty.Register(nameof(IconKind), typeof(PackIconKind), typeof(MFButton),
                new PropertyMetadata(PackIconKind.Cycling));

        public PackIconKind IconKind
        {
            get => (PackIconKind)GetValue(IconKindProperty);
            set => SetValue(IconKindProperty, value);
        }

        public static readonly DependencyProperty TextDataProperty =
            DependencyProperty.Register(nameof(TextData), typeof(string), typeof(MFButton),
                new PropertyMetadata(""));

        public string TextData
        {
            get => (string)GetValue(TextDataProperty);
            set => SetValue(TextDataProperty, value);
        }
    }
}
