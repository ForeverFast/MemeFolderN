using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using MemeFolderN.MFViewModelsBase.Commands;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class FolderVMBase : BasePageViewModel, IFolderVM, IFolder
    {
        public RelayCommand FolderLoadCommand => _folderLoadCommand ?? (_folderLoadCommand =
            new RelayCommandAction(FolderLoadMethod));
        protected virtual void FolderLoadMethod()
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвана прогрузка папок для {this.Id} / {this.Title}");
#endif
        }


        public RelayCommand FolderAddCommand => _folderAddCommand ?? (_folderAddCommand =
            new RelayCommandAction(FolderAddMethod));

        protected virtual void FolderAddMethod()
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано добавление папки для {this.Id} / {this.Title}");
#endif
        }

        public RelayCommand FolderAddNonParametersCommand => _folderAddNonParametersCommand ?? (_folderAddNonParametersCommand =
            new RelayCommandAction(FolderAddNonParametersMethod));

        protected virtual void FolderAddNonParametersMethod()
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано добавление коренной папки без параметров.");
#endif
        }

        public RelayCommand FolderChangeCommand => _folderChangeCommand ?? (_folderChangeCommand =
            new RelayCommandAction<FolderVMBase>(FolderChangeMethod));

        protected virtual void FolderChangeMethod(FolderVMBase folderVMBase)
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано изменение папки {folderVMBase.Id} / {folderVMBase.Title} для {this.Id} / {this.Title}");
#endif
        }


        public RelayCommand FolderDeleteCommand => _folderDeleteCommand ?? (_folderDeleteCommand =
            new RelayCommandAction<FolderVMBase>(FolderDeleteMethod));

        protected virtual void FolderDeleteMethod(FolderVMBase folderVMBase)
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано удаление папки {folderVMBase.Id} / {folderVMBase.Title} для {this.Id} / {this.Title}");
#endif
        }

        #region Поля для хранения значений свойств
        private RelayCommand _folderLoadCommand;
        private RelayCommand _folderAddCommand;
        private RelayCommand _folderAddNonParametersCommand;
        private RelayCommand _folderChangeCommand;
        private RelayCommand _folderDeleteCommand;
        #endregion
    }
}
