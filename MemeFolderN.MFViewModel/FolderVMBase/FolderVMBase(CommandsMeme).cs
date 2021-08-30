using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using MemeFolderN.MFViewModelsBase.Commands;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class FolderVMBase : BasePageViewModel, IFolderVM, IFolder
    {
        public RelayCommand MemeLoadCommand => _memeLoadCommand ?? (_memeLoadCommand =
            new RelayCommandAction(MemeLoadMethod));

        protected virtual void MemeLoadMethod()
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвана прогрузка папок для {this.Id} / {this.Title}");
#endif
        }

        public RelayCommand MemeAddCommand => _memeAddCommand ?? (_memeAddCommand =
            new RelayCommandAction(MemeAddMethod));

        protected virtual void MemeAddMethod()
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано добавление мема для {this.Id} / {this.Title}");
#endif
        }

        public RelayCommand MemeAddNonParametersCommand => _memeAddNonParametersCommand ?? (_memeAddNonParametersCommand =
            new RelayCommandAction(MemeAddNonParametersMethod));

        protected virtual void MemeAddNonParametersMethod()
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано добавление мема без параметров для {this.Id} / {this.Title}");
#endif
        }

        public RelayCommand MemeDeleteCommand => _memeDeleteCommand ?? (_memeDeleteCommand =
            new RelayCommandAction<MemeVMBase>(MemeDeleteMethod));

        protected virtual void MemeDeleteMethod(MemeVMBase memeVMBase)
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано удаление мема {this.Id} / {this.Title}");
#endif
        }

        #region Поля для хранения значений свойств
        private RelayCommand _memeLoadCommand;
        private RelayCommand _memeAddCommand;
        private RelayCommand _memeAddNonParametersCommand;
        private RelayCommand _memeDeleteCommand;
        #endregion
    }
}
