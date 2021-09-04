using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace TreeViewExample
{
    public class BindingTagSourceCollectionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is FrameworkElement element)
            {
                if (element.Resources[typeof(BindingTagSourceCollectionConverter)] is not TagCollectionViewSource coll)
                {
                    coll = new TagCollectionViewSource();
                    //coll.IsLiveFilteringRequested = true;
                    //coll.LiveFilteringProperties.Add(nameof(Node.Id));
                    element.Resources[typeof(BindingTagSourceCollectionConverter)] = coll;
                    coll.Filter += (FilterEventHandler)values[1];
                }

                coll.Source = values[2];
                coll.Tag = values[3];
                coll.View.Refresh();
                return coll;
            }

            return DependencyProperty.UnsetValue; ;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static BindingTagSourceCollectionConverter Instance { get; } = new BindingTagSourceCollectionConverter();
    }

    [MarkupExtensionReturnType(typeof(BindingTagSourceCollectionConverter))]
    public class BindingTagSourceCollectionConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
            => BindingTagSourceCollectionConverter.Instance;
    }

}
