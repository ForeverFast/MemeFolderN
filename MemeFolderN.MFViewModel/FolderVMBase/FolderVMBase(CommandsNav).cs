using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using MemeFolderN.MFViewModelsBase.Commands;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class FolderVMBase : BasePageViewModel, IFolderVM, IFolder
    {
        public RelayCommand NavigationByFolderCommand => _navigationByFolderCommand ?? (_navigationByFolderCommand =
           new RelayCommandAction<IFolder>(NavigationByFolderMethod, (f) => _navigationService.CanNavigate(f?.Id.ToString())));

        protected virtual void NavigationByFolderMethod(IFolder folder)
        {
#if DEBUG
            ShowMetod($"Вызван метод навигации к папке по ключу {folder?.Id}.");
#endif
        }

        #region Поля для хранения значений свойств
        private RelayCommand _navigationByFolderCommand;
        #endregion
    }
}
