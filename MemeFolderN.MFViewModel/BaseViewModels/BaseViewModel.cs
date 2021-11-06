using MemeFolderN.MFViewModelsBase.Commands;
using System;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MemeFolderN.MFViewModelsBase.BaseViewModels
{
    public abstract class BaseViewModel : OnPropertyChangedClass, IDisposable
    {
        protected bool _isBusy;
        protected bool _isLoaded;
        protected bool _showExecutableMethod;
        protected bool _disposed = false;

        public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }
        public bool IsLoaded { get => _isLoaded; set => SetProperty(ref _isLoaded, value); }

        public void Dispose()
        {
            CleanUp(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void CleanUp(bool clean);

#if DEBUG
        /// <summary>Показывать исполняемые методы</summary>
        protected bool ShowExecutableMethod { get => _showExecutableMethod; set => SetProperty(ref _showExecutableMethod, value); }

        public RelayCommand ShowExecutableMethodCommand => _showExecutableMethodCommand ?? (_showExecutableMethodCommand =
            new RelayCommandAction(ShowExecutableMethodMethod));

        protected virtual void ShowExecutableMethodMethod()
        {
            ShowExecutableMethod = !ShowExecutableMethod;
        }

        /// <summary>Показ вызванного метода</summary>
        /// <param name="metodName">Название метода</param>
        protected void ShowMetod(string message = null, [CallerMemberName] string metodName = null)
        {
            if (ShowExecutableMethod)
                MessageBox.Show($"Вызван метод {metodName}"
                    + (message != null ? $": {message}" : ""));
        }

        #region Поля для хранения значений свойств
        private RelayCommand _showExecutableMethodCommand;
        #endregion
#endif
    }
}
