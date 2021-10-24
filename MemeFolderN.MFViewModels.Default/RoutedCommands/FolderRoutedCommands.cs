using System.Windows.Input;

namespace MemeFolderN.MFViewModels.Default.RoutedCommands
{
    public static class FolderRoutedCommands
    {
        public static RoutedCommand NavigationByFolderRoutedCommand { get; }
            = new RoutedUICommand("Открыть", nameof(NavigationByFolderRoutedCommand), typeof(FolderRoutedCommands));

        public static RoutedCommand FolderAddNonParametersRoutedCommand { get; }
            = new RoutedUICommand("Создать папку", nameof(FolderAddNonParametersRoutedCommand), typeof(FolderRoutedCommands));

        public static RoutedCommand FolderAddRoutedCommand { get; }
           = new RoutedUICommand("Создать папку с параметрами", nameof(FolderAddRoutedCommand), typeof(FolderRoutedCommands));

        public static RoutedCommand FolderChangeRoutedCommand { get; }
            = new RoutedUICommand("Изменить", nameof(FolderChangeRoutedCommand), typeof(FolderRoutedCommands));

        public static RoutedCommand FolderDeleteRoutedCommand { get; }
           = new RoutedUICommand("Открыть", nameof(FolderDeleteRoutedCommand), typeof(FolderRoutedCommands));
    }
}
