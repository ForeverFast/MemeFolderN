using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
using System.Windows.Input;

namespace MemeFolderN.MFViews.Wpf.Behaviors
{
    public class ScrollViewerPreviewMouseWheelBehavior : Behavior<ScrollViewer>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewMouseWheel += tagListBox_PreviewMouseWheel;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseWheel -= tagListBox_PreviewMouseWheel;
        }

        public static void tagListBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer sv = (ScrollViewer)sender;
            double offset = sv.ContentHorizontalOffset + (e.Delta / 120);
            sv.ScrollToHorizontalOffset(offset);
        }
    }
}
