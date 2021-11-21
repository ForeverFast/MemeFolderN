using MemeFolderN.MFViewModels.Common.Commands;

namespace MemeFolderN.MFViewModels.Common.Abstractions
{
    public interface IFolderVM
    {
        IFolder SelectedFolder { get; set; }
        IMeme SelectedMeme { get; set; }

       
        RelayCommand FolderLoadCommand { get; }
        RelayCommand FolderAddCommand { get; }
        RelayCommand FolderAddNonParametersCommand { get; }
        RelayCommand FolderChangeCommand { get; }
        RelayCommand FolderDeleteCommand { get; }


        RelayCommand MemeLoadCommand { get; }
        RelayCommand MemeAddCommand { get; }
        RelayCommand MemeAddNonParametersCommand { get; }
        RelayCommand MemeChangeCommand { get; }
        RelayCommand MemeDeleteCommand { get; }
    }
}
