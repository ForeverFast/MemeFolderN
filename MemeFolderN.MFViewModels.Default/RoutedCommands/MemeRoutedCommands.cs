using System.Windows.Input;

namespace MemeFolderN.MFViewModels.Default.RoutedCommands
{
    public static class MemeRoutedCommands
    {
        public static RoutedCommand MemeAddNonParametersRoutedCommand { get; }
            = new RoutedUICommand("Создать мем", nameof(MemeAddNonParametersRoutedCommand), typeof(MemeRoutedCommands));

        public static RoutedCommand MemeAddRoutedCommand { get; }
            = new RoutedUICommand("Создать мем с параметрами", nameof(MemeAddRoutedCommand), typeof(MemeRoutedCommands));

        public static RoutedCommand MemeOpenRoutedCommand { get; }
            = new RoutedUICommand("Открыть", nameof(MemeOpenRoutedCommand), typeof(MemeRoutedCommands));

        public static RoutedCommand MemeOpenInExploerRoutedCommand { get; }
            = new RoutedUICommand("Открыть в проводнике", nameof(MemeOpenInExploerRoutedCommand), typeof(MemeRoutedCommands));

        public static RoutedCommand MemeCopyRoutedCommand { get; }
            = new RoutedUICommand("Скорпировать в буфер", nameof(MemeCopyRoutedCommand), typeof(MemeRoutedCommands));

        public static RoutedCommand MemeChangeRoutedCommand { get; }
            = new RoutedUICommand("Изменить", nameof(MemeChangeRoutedCommand), typeof(MemeRoutedCommands));

        public static RoutedCommand MemeDeleteRoutedCommand { get; }
           = new RoutedUICommand("Удалить", nameof(MemeDeleteRoutedCommand), typeof(MemeRoutedCommands));




    }
}
