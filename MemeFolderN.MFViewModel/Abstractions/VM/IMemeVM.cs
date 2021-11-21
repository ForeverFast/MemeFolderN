using MemeFolderN.MFViewModels.Common.Commands;

namespace MemeFolderN.MFViewModels.Common.Abstractions
{
    public interface IMemeVM
    {
        RelayCommand MemeOpenCommand { get; }
        RelayCommand MemeCopyCommand { get; }
    }
}
