using System;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MemeFolderN.MFViewModelsBase.BaseViewModels
{
    public abstract class BaseViewModel : OnPropertyChangedClass, IDisposable
    {
        protected bool _isBusy;
        protected bool _isLoaded;
        public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }
        public bool IsLoaded { get => _isLoaded; set => SetProperty(ref _isLoaded, value); }

        public abstract void Dispose();

#if DEBUG
        /// <summary>Показывать исполняемые методы</summary>
        protected bool ShowExecutableMethod = true;


        /// <summary>Показ вызванного метода</summary>
        /// <param name="metodName">Название метода</param>
        protected void ShowMetod(string message = null, [CallerMemberName] string metodName = null)
        {
            if (ShowExecutableMethod)
                MessageBox.Show($"Вызван метод {metodName}"
                    + (message != null ? $": {message}" : ""));
        }
#endif
    }
}
