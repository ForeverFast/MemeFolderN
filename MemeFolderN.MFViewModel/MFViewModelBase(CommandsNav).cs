using MemeFolderN.MFViewModels.Common.Abstractions;
using MemeFolderN.MFViewModels.Common.BaseViewModels;
using MemeFolderN.MFViewModels.Common.Commands;

namespace MemeFolderN.MFViewModels.Common
{
    public abstract partial class MFViewModelBase : BaseWindowViewModel, IMFViewModel
    {
        public RelayCommand NavigationByFolderCommand => _navigationByFolderCommand ?? (_navigationByFolderCommand =
           new RelayCommandAction<IFolder>(NavigationByFolderMethod));

        protected virtual void NavigationByFolderMethod(IFolder folder)
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызван метод навигации к папке по ключу {folder?.Id}.");
#endif
        }

        public RelayCommand NavigationByMemeTagCommand => _navigationByMemeTagCommand ?? (_navigationByMemeTagCommand =
          new RelayCommandAction<IMemeTag>(NavigationByMemeTagMethod));

        protected virtual void NavigationByMemeTagMethod(IMemeTag memeTag)
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызван метод навигации к мему по тегу {memeTag?.Id} / {memeTag?.Title}.");
#endif
        }

        #region Поля для хранения значений свойств
        private RelayCommand _navigationByFolderCommand;
        private RelayCommand _navigationByMemeTagCommand;
        #endregion

    }
}
