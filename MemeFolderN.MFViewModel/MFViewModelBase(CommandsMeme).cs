using MemeFolderN.MFViewModels.Common.Abstractions;
using MemeFolderN.MFViewModels.Common.BaseViewModels;
using MemeFolderN.MFViewModels.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Common
{
    public abstract partial class MFViewModelBase : BaseWindowViewModel, IMFViewModel
    {
        public RelayCommand MemeLoadCommand => _memeLoadCommand ?? (_memeLoadCommand =
            new RelayCommandAction(MemeLoadMethod));

        protected virtual void MemeLoadMethod()
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвана прогрузка мемов.");
#endif
        }

        public RelayCommand MemeAddCommand => _memeAddCommand ?? (_memeAddCommand =
            new RelayCommandAction<FolderVMBase>(MemeAddMethod));

        protected virtual void MemeAddMethod(FolderVMBase folderVMBase)
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано добавление мема для {folderVMBase.Id} / {folderVMBase.Title}");
#endif
        }

        public RelayCommand MemeAddNonParametersCommand => _memeAddNonParametersCommand ?? (_memeAddNonParametersCommand =
            new RelayCommandAction<FolderVMBase>(MemeAddNonParametersMethod));

        protected virtual void MemeAddNonParametersMethod(FolderVMBase folderVMBase)
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано добавление мема без параметров для {folderVMBase.Id} / {folderVMBase.Title}");
#endif
        }

        public RelayCommand MemeChangeCommand => _memeChangeCommand ?? (_memeChangeCommand =
           new RelayCommandAction<MemeVMBase>(MemeChangeMethod));

        protected virtual void MemeChangeMethod(MemeVMBase memeVMBase)
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано изменение мема {memeVMBase.Id} / {memeVMBase.Title}");
#endif
        }

        public RelayCommand MemeDeleteCommand => _memeDeleteCommand ?? (_memeDeleteCommand =
            new RelayCommandAction<MemeVMBase>(MemeDeleteMethod));

        protected virtual void MemeDeleteMethod(MemeVMBase memeVMBase)
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано удаление мема {memeVMBase.Id} / {memeVMBase.Title}");
#endif
        }


        public RelayCommand MemeDeleteTagCommand => _memeDeleteTagCommand ?? (_memeDeleteTagCommand =
            new RelayCommandAction<object>(MemeDeleteTagMethod));

        protected virtual void MemeDeleteTagMethod(object data)
        {
            object[] coll = (object[])data;
            MemeVMBase memeVMBase = coll[0] as MemeVMBase;
            MemeTagVMBase memeTagVMBase = coll[1] as MemeTagVMBase;
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызвано удаление тега {memeTagVMBase.Id} / {memeTagVMBase.Title} из мема {memeVMBase.Id} / {memeVMBase.Title}");
#endif
        }

        public RelayCommand MemeOpenCommand => _memeOpenCommand ?? (_memeOpenCommand =
           new RelayCommandAction<MemeVMBase>(MemeOpenMethod, (mVm) => !string.IsNullOrEmpty(mVm.ImagePath)));

        protected virtual void MemeOpenMethod(MemeVMBase memeVMBase)
        {
#if DEBUG
            ShowMetod($"Вызвано открытие изображения мема {memeVMBase.Id} / {memeVMBase.Title}");
#endif
        }

        public RelayCommand MemeOpenInExplorerCommand => _memeOpenInExplorerCommand ?? (_memeOpenInExplorerCommand =
           new RelayCommandAction<MemeVMBase>(MemeOpenInExplorerMethod, (mVm) => !string.IsNullOrEmpty(mVm.ImagePath)));

        protected virtual void MemeOpenInExplorerMethod(MemeVMBase memeVMBase)
        {
#if DEBUG
            ShowMetod($"Вызвано открытие папки для мема {memeVMBase.Id} / {memeVMBase.Title}");
#endif
        }

        public RelayCommand MemeCopyCommand => _memeCopyCommand ?? (_memeCopyCommand =
            new RelayCommandAction<MemeVMBase>(MemeCopyMethod, (mVm) => !string.IsNullOrEmpty(mVm.ImagePath)));

        protected virtual void MemeCopyMethod(MemeVMBase memeVMBase)
        {
#if DEBUG
            ShowMetod($"Вызвано открытие изображения мема {memeVMBase.Id} / {memeVMBase.Title}");
#endif
        }


        #region Поля для хранения значений свойств
        private RelayCommand _memeLoadCommand;
        private RelayCommand _memeAddCommand;
        private RelayCommand _memeAddNonParametersCommand;
        private RelayCommand _memeChangeCommand;
        private RelayCommand _memeDeleteCommand;
        private RelayCommand _memeDeleteTagCommand;
        private RelayCommand _memeOpenCommand;
        private RelayCommand _memeOpenInExplorerCommand;
        private RelayCommand _memeCopyCommand;
        #endregion
    }
}
