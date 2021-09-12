using MemeFolderN.MFViewModelsBase.Commands;

namespace MemeFolderN.MFViewModelsBase.Abstractions
{
    public interface IMemeVM
    {
        RelayCommand MemeOpenCommand { get; }
        RelayCommand MemeCopyCommand { get; }
    }
}
