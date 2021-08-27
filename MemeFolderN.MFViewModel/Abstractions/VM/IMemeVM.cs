using MemeFolderN.MFViewModelsBase.Commands;

namespace MemeFolderN.MFViewModelsBase.Abstractions
{
    public interface IMemeVM : IMeme
    {
        RelayCommand MemeChangeCommand { get; }
        RelayCommand MemeOpenCommand { get; }
        RelayCommand MemeCopyCommand { get; }

        RelayCommand MemeTagLoadCommand { get; }
    }
}
