using MemeFolderN.MFViewModels.Common.Abstractions;
using MemeFolderN.MFViewModels.Common.BaseViewModels;
using System;
using System.Runtime.CompilerServices;

namespace MemeFolderN.MFViewModels.Common
{
    public abstract partial class MFViewModelBase : BaseWindowViewModel, IMFViewModel
    {
        public event ExceptionHandler ExceptionEvent;

        /// <summary>Вспомогательный метод для передачи сообщения об ошибке</summary>
        /// <param name="exc">Параметры ошибки</param>
        /// <param name="nameMetod">Метод отправивший сообщение</param>
        public void OnException(Exception exc, [CallerMemberName] string nameMetod = null)
            => ExceptionEvent?.Invoke(this, nameMetod, exc);
    }
}
