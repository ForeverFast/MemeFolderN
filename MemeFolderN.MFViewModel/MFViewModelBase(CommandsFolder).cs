using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using MemeFolderN.MFViewModelsBase.Commands;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class MFViewModelBase : BaseWindowViewModel, IMFViewModel
    {
        public RelayCommand FolderRootsCommand => _folderRootsCommand ?? (_folderRootsCommand =
           new RelayCommandAction(FolderRootsMethod));

        protected virtual void FolderRootsMethod()
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызван метод прогрузки коренных папок.");
#endif
        }

        public RelayCommand FolderAddCommand => _folderAddCommand ?? (_folderAddCommand =
            new RelayCommandAction(FolderAddMethod));

        protected virtual void FolderAddMethod()
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано добавление коренной папки.");
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

        #region Поля для хранения значений свойств
        private RelayCommand _folderRootsCommand;
        private RelayCommand _folderAddCommand;
        private RelayCommand _folderAddNonParametersCommand;
        #endregion
    }
}
