using MemeFolderN.MFViewModelsBase.Commands;
using System;
using System.Collections.ObjectModel;

namespace MemeFolderN.MFViewModelsBase.Abstractions
{
    /// <summary>Делегат события возникновения исключения</summary>
    /// <param name="sender">Источник исключения</param>
    /// <param name="nameMetod">Метод где возникло исключение</param>
    /// <param name="exc">Параметры исключения</param>
    public delegate void ExceptionHandler(object sender, string nameMetod, Exception exc);

    public interface IMFViewModel
    {
        ObservableCollection<IFolder> RootFolders { get; }
        ObservableCollection<IMemeTag> MemeTags { get; }
       
        IMemeTag SelectedMemeTag { get; set; }

        RelayCommand FolderRootsCommand { get; }
        RelayCommand FolderAddCommand { get; }
        RelayCommand FolderAddNonParametersCommand { get; }
        

        RelayCommand MemeTagLoadCommand { get; }
        RelayCommand MemeTagAddCommand { get; }
        RelayCommand MemeTagChangeCommand { get; }
        RelayCommand MemeTagDeleteCommand { get; }


        RelayCommand NavigationByFolderCommand { get; }
        RelayCommand NavigationByMemeTagCommand { get; }

    }
}
