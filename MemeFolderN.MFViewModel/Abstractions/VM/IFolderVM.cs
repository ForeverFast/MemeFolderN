using MemeFolderN.MFViewModelsBase.Commands;

namespace MemeFolderN.MFViewModelsBase.Abstractions
{
    public interface IFolderVM : IFolder
    {
        IFolder SelectedFolder { get; set; }
        IMeme SelectedMeme { get; set; }

       
        RelayCommand FolderFoldersCommand { get; }
        RelayCommand FolderAddCommand { get; }
        RelayCommand FolderChangeCommand { get; }
        RelayCommand FolderDeleteCommand { get; }

        RelayCommand MemeLoadCommand { get; }
        RelayCommand MemeAddCommand { get; }
        RelayCommand MemeDeleteCommand { get; }
    }
}
