using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFViewModelsBase.Commands;

namespace MemeFolderN.MFViewModelsBase.Abstractions
{
    public interface IFolderVM : IFolder
    {
        IMFModel model { get; }

        IFolder SelectedFolder { get; set; }
        IMeme SelectedMeme { get; set; }

        //RelayCommand FolderRootsCommand { get; }
        RelayCommand FolderFoldersCommand { get; }
        RelayCommand FolderAddCommand { get; }
        RelayCommand FolderChangeCommand { get; }
        RelayCommand FolderDeleteCommand { get; }

        RelayCommand MemeLoadCommand { get; }
        RelayCommand MemeAddCommand { get; }
    }
}
