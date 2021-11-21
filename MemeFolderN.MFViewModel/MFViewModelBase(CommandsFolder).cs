using MemeFolderN.MFViewModels.Common.Abstractions;
using MemeFolderN.MFViewModels.Common.BaseViewModels;
using MemeFolderN.MFViewModels.Common.Commands;

namespace MemeFolderN.MFViewModels.Common
{
    public abstract partial class MFViewModelBase : BaseWindowViewModel, IMFViewModel
    {
        public RelayCommand FolderLoadCommand => _folderLoadCommand ?? (_folderLoadCommand =
            new RelayCommandAction(FolderLoadMethod));
        protected virtual void FolderLoadMethod()
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвана прогрузка папок.");
#endif
        }

        public RelayCommand FolderAddCommand => _folderAddCommand ?? (_folderAddCommand =
            new RelayCommandAction<FolderVMBase>(FolderAddMethod));

        protected virtual void FolderAddMethod(FolderVMBase folderVMBase)
        {
            IsBusy = true;
#if DEBUG
            if (folderVMBase != null)
                ShowMetod($"Вызвано добавление папки для {folderVMBase.Id} / {folderVMBase.Title}");
            else
                ShowMetod($"Вызвано добавление папки для корневого раздела");
#endif
        }

        public RelayCommand FolderAddNonParametersCommand => _folderAddNonParametersCommand ?? (_folderAddNonParametersCommand =
            new RelayCommandAction<FolderVMBase>(FolderAddNonParametersMethod));

        protected virtual void FolderAddNonParametersMethod(FolderVMBase folderVMBase)
        {
            IsBusy = true;
#if DEBUG
            if (folderVMBase != null)
                ShowMetod($"Вызвано добавление папки без параметров для {folderVMBase.Id} / {folderVMBase.Title}");
            else
                ShowMetod($"Вызвано добавление папки без параметров для корневого раздела");
#endif
        }

        public RelayCommand FolderChangeCommand => _folderChangeCommand ?? (_folderChangeCommand =
            new RelayCommandAction<FolderVMBase>(FolderChangeMethod));

        protected virtual void FolderChangeMethod(FolderVMBase folderVMBase)
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано изменение папки {folderVMBase.Id} / {folderVMBase.Title} для {folderVMBase.ParentFolderId}");
#endif
        }


        public RelayCommand FolderDeleteCommand => _folderDeleteCommand ?? (_folderDeleteCommand =
            new RelayCommandAction<FolderVMBase>(FolderDeleteMethod));

        protected virtual void FolderDeleteMethod(FolderVMBase folderVMBase)
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано удаление папки {folderVMBase.Id} / {folderVMBase.Title} для {folderVMBase.ParentFolderId}");
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
