using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using MemeFolderN.MFViewModelsBase.Commands;
using System;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class FolderVMBase : BasePageViewModel, IFolderVM, IFolder
    {
        //public RelayCommand FolderRootsCommand => _folderRootsCommand ?? ( _folderRootsCommand =
        //    new RelayCommandAction(FolderRoots));

//        protected virtual void FolderRoots()
//        {
//#if DEBUG
//            ShowMetod($"");
//#endif
//        }

        public RelayCommand FolderFoldersCommand => _folderFolderCommand ?? (_folderFolderCommand =
            new RelayCommandAction(FolderFoldersMethod));
        protected virtual void FolderFoldersMethod()
        {
#if DEBUG
            ShowMetod($"Вызвана прогрузка папок для {this.Id} / {this.Title}");
#endif
        }


        public RelayCommand FolderAddCommand => _folderAddCommand ?? (_folderAddCommand =
            new RelayCommandAction(FolderAddMethod));

        protected virtual void FolderAddMethod()
        {
#if DEBUG
            ShowMetod($"Вызвано добавление папки для {this.Id} / {this.Title}");
#endif
        }

        public RelayCommand FolderChangeCommand => _folderChangeCommand ?? (_folderChangeCommand =
            new RelayCommandAction<FolderVMBase>(FolderChangeMethod));

        protected virtual void FolderChangeMethod(FolderVMBase folderVMBase)
        {
#if DEBUG
            ShowMetod($"Вызвано изменение папки {folderVMBase.Id} / {folderVMBase.Title} для {this.Id} / {this.Title}");
#endif
        }


        public RelayCommand FolderDeleteCommand => _folderDeleteCommand ?? (_folderDeleteCommand =
            new RelayCommandAction<FolderVMBase>(FolderDeleteMethod));

        protected virtual void FolderDeleteMethod(FolderVMBase folderVMBase)
        {
#if DEBUG
            ShowMetod($"Вызвано удаление папки {folderVMBase.Id} / {folderVMBase.Title} для {this.Id} / {this.Title}");
#endif
        }


        public RelayCommand MemeLoadCommand => _memeLoadCommand ?? (_memeLoadCommand =
            new RelayCommandAction(MemeLoadMethod));

        protected virtual void MemeLoadMethod()
        {
#if DEBUG
            ShowMetod($"Вызвана прогрузка папок для {this.Id} / {this.Title}");
#endif
        }

        public RelayCommand MemeAddCommand => _memeAddCommand ?? (_memeAddCommand =
            new RelayCommandAction(MemeAddMethod));

        protected virtual void MemeAddMethod()
        {
#if DEBUG
            ShowMetod($"Вызвано добавление мема для {this.Id} / {this.Title}");
#endif
        }

        private RelayCommand _folderFolderCommand;
        private RelayCommand _folderAddCommand;
        private RelayCommand _folderChangeCommand;
        private RelayCommand _folderDeleteCommand;

        private RelayCommand _memeLoadCommand;
        private RelayCommand _memeAddCommand;
    }
}
