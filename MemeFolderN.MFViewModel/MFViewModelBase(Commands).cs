using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using MemeFolderN.MFViewModelsBase.Commands;
using System;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class MFViewModelBase : BaseWindowViewModel, IMFViewModel
    {
        public RelayCommand FolderRootsCommand => _folderRootsCommand ?? (_folderRootsCommand =
            new RelayCommandAction(FolderRootsMethod));

        protected virtual void FolderRootsMethod()
        {
#if DEBUG
            ShowMetod($"Вызван метод прогрузки коренных папок.");
#endif
        }


        public RelayCommand MemeTagLoadCommand => _memeTagLoadCommand ?? (_memeTagLoadCommand =
            new RelayCommandAction(MemeTagLoadMethod));

        protected virtual void MemeTagLoadMethod()
        {
#if DEBUG
            ShowMetod($"Вызван метод прогрузки тегов.");
#endif
        }


        public RelayCommand MemeTagAddCommand => _memeTagAddCommand ?? (_memeTagAddCommand =
            new RelayCommandAction(MemeTagAddMethod));

        protected virtual void MemeTagAddMethod()
        {
#if DEBUG
            ShowMetod($"Вызван метод добавления тега.");
#endif
        }

        public RelayCommand MemeTagChangeCommand => _memeTagChangeCommand ?? (_memeTagChangeCommand =
            new RelayCommandAction<MemeTagVMBase>(MemeTagChangeMethod));

        protected virtual void MemeTagChangeMethod(MemeTagVMBase memeTagVMBase)
        {
#if DEBUG
            ShowMetod($"Вызван метод изменения тега.");
#endif
        }

        public RelayCommand MemeTagDeleteCommand => _memeTagDeleteCommand ?? (_memeTagDeleteCommand =
            new RelayCommandAction<MemeTagVMBase>(MemeTagDeleteMethod));

        protected virtual void MemeTagDeleteMethod(MemeTagVMBase memeTagVMBase)
        {
#if DEBUG
            ShowMetod($"Вызван метод изменения тега.");
#endif
        }

        #region Поля для хранения значений свойств
        private RelayCommand _folderRootsCommand;

        private RelayCommand _memeTagLoadCommand;
        private RelayCommand _memeTagAddCommand;
        private RelayCommand _memeTagChangeCommand;
        private RelayCommand _memeTagDeleteCommand;
        #endregion
    }
}
