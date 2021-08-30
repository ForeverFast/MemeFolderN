using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using MemeFolderN.MFViewModelsBase.Commands;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class MemeVMBase : BaseNavigationViewModel, IMemeVM, IMeme
    {
        public RelayCommand MemeChangeCommand => _memeChangeCommand ?? (_memeChangeCommand =
            new RelayCommandAction<MemeVMBase>(MemeChangeMethod));

        protected virtual void MemeChangeMethod(MemeVMBase memeVMBase)
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано изменение мема {this.Id} / {this.Title}");
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

        public RelayCommand MemeOpenCommand => _memeOpenCommand ?? (_memeOpenCommand =
            new RelayCommandAction(MemeOpenMethod, () => !string.IsNullOrEmpty(this.ImagePath)));

        protected virtual void MemeOpenMethod()
        {
#if DEBUG
            ShowMetod($"Вызвано открытие изображения мема {this.Id} / {this.Title}");
#endif
        }

        public RelayCommand MemeCopyCommand => _memeCopyCommand ?? (_memeCopyCommand =
            new RelayCommandAction(MemeCopyMethod, () => !string.IsNullOrEmpty(this.ImagePath)));

        protected virtual void MemeCopyMethod()
        {
#if DEBUG
            ShowMetod($"Вызвано открытие изображения мема {this.Id} / {this.Title}");
#endif
        }

        public RelayCommand MemeTagLoadCommand => _memeTagLoadCommand ?? (_memeTagLoadCommand =
            new RelayCommandAction(MemeTagLoadMethod));

        protected virtual void MemeTagLoadMethod()
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано прогрузка тегов для мема {this.Id} / {this.Title}");
#endif
        }


        #region Поля для хранения значений свойств
        private RelayCommand _memeChangeCommand;
        private RelayCommand _memeDeleteCommand;
        private RelayCommand _memeOpenCommand;
        private RelayCommand _memeCopyCommand;

        private RelayCommand _memeTagLoadCommand;
        #endregion
    }
}
