using System.Windows;
using System.Windows.Data;

namespace MemeFolderN.MFViews.Extentions
{
    public delegate void PropertyChangedHandler<T>(T sender, DependencyPropertyChangedEventArgs e)
      where T : DependencyObject;
    public class TagCollectionViewSource : CollectionViewSource
    {
        /// <summary>
        /// Свойство для любых дополнительных данных.
        /// </summary>
        public object Tag
        {
            get => GetValue(TagProperty);
            set => SetValue(TagProperty, value);
        }

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="Tag"/>.</summary>
        public static readonly DependencyProperty TagProperty =
            DependencyProperty.Register(
                nameof(Tag),
                typeof(object),
                typeof(TagCollectionViewSource),
                new PropertyMetadata(null));


        /// <summary>
        /// Делегат метода вызываемого при изменении свойства <see cref="Tag"/>.
        /// </summary>
        public PropertyChangedHandler<TagCollectionViewSource> PropertyChanged
        {
            get => (PropertyChangedHandler<TagCollectionViewSource>)GetValue(PropertyChangedProperty);
            set => SetValue(PropertyChangedProperty, value);
        }

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="PropertyChanged"/>.</summary>
        public static readonly DependencyProperty PropertyChangedProperty =
            DependencyProperty.Register(
                nameof(PropertyChanged),
                typeof(PropertyChangedHandler<TagCollectionViewSource>),
                typeof(TagCollectionViewSource),
                new PropertyMetadata(null));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            PropertyChanged?.Invoke(this, e);
        }

    }
}
