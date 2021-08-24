using MemeFolderN.MFViewModelsBase.Commands;
using System.Collections.ObjectModel;

namespace MemeFolderN.MFViewModelsBase.Abstractions
{
    public interface IMFViewModel
    {
        ObservableCollection<IFolder> RootFolders { get; }
        ObservableCollection<IMemeTag> MemeTags { get; }
       
        IMemeTag SelectedMemeTag { get; set; }

        RelayCommand FolderRootsCommand { get; }

        RelayCommand MemeTagLoadCommand { get; }
        RelayCommand MemeTagAddCommand { get; }
        RelayCommand MemeTagChangeCommand { get; }
        RelayCommand MemeTagDeleteCommand { get; }
    }
}
