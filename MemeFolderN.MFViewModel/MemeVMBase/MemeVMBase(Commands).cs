using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using MemeFolderN.MFViewModelsBase.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class MemeVMBase : BaseNavigationViewModel, IMemeVM, IMeme
    {

        public RelayCommand MemeChangeCommand => _memeChangeCommand ?? (_memeChangeCommand =
            new RelayCommandAction<MemeVMBase>(MemeChangeMethod));

        protected virtual void MemeChangeMethod(MemeVMBase memeVMBase)
        {
#if DEBUG
            ShowMetod($"Вызвано изменение мема {this.Id} / {this.Title}");
#endif
        }


        public RelayCommand MemeDeleteCommand => _memeDeleteCommand ?? (_memeDeleteCommand =
            new RelayCommandAction<MemeVMBase>(MemeChangeMethod));

        protected virtual void MemeDeleteMethod(MemeVMBase memeVMBase)
        {
#if DEBUG
            ShowMetod($"Вызвано удаление мема {this.Id} / {this.Title}");
#endif
        }


        public RelayCommand MemeOpenCommand => _memeOpenCommand ?? (_memeOpenCommand =
            new RelayCommandAction(MemeOpenMethod));

        protected virtual void MemeOpenMethod()
        {
#if DEBUG
            ShowMetod($"Вызвано открытие изображения мема {this.Id} / {this.Title}");
#endif
        }

        public RelayCommand MemeCopyCommand => _memeCopyCommand ?? (_memeCopyCommand =
            new RelayCommandAction(MemeCopyMethod));

        protected virtual void MemeCopyMethod()
        {
#if DEBUG
            ShowMetod($"Вызвано открытие изображения мема {this.Id} / {this.Title}");
#endif
        }

        private RelayCommand _memeChangeCommand;
        private RelayCommand _memeDeleteCommand;
        private RelayCommand _memeOpenCommand;
        private RelayCommand _memeCopyCommand;

       
    }
}
