using System.Windows.Input;

namespace MemeFolderN.MFViewModels.Default.RoutedCommands
{
    public static class MemeTagRoutedCommands
    {
        public static RoutedCommand NavigationByMemeTagRoutedCommand { get; }
            = new RoutedUICommand("Открыть", nameof(NavigationByMemeTagRoutedCommand), typeof(MemeTagRoutedCommands));

        public static RoutedCommand MemeTagAddRoutedCommand { get; }
           = new RoutedUICommand("Создать", nameof(MemeTagAddRoutedCommand), typeof(MemeTagRoutedCommands));

        public static RoutedCommand MemeTagChangeRoutedCommand { get; }
            = new RoutedUICommand("Изменить", nameof(MemeTagChangeRoutedCommand), typeof(MemeTagRoutedCommands));

        public static RoutedCommand MemeTagDeleteRoutedCommand { get; }
           = new RoutedUICommand("Открыть", nameof(MemeTagDeleteRoutedCommand), typeof(MemeTagRoutedCommands));
    }
}
