using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MemeFolderN.MFViewModels.Common.Commands
{
    #region Делегаты для методов WPF команд
    /// <summary>Делегат исполняющего метода с параметром типа object</summary>
    /// <param name="parameter">Параметр метода</param>
    public delegate void ExecuteHandler(object parameter);
    /// <summary>Делегат проверяющего метода с параметром типа object</summary>
    /// <param name="parameter">Параметр метода</param>
    public delegate bool CanExecuteHandler(object parameter);
    #endregion

    #region Класс команд - RelayCommand
    /// <summary>Класс реализующий интерфейс ICommand для создания WPF команд</summary>
    public class RelayCommand : ICommand
    {
        private readonly CanExecuteHandler _canExecute = _ => true;
        private readonly ExecuteHandler _onExecute;
        private readonly EventHandler _requerySuggested;

        /// <summary>Событие извещающее об изменении состояния команды</summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>Конструктор команды</summary>
        /// <param name="execute">Выполняемый метод команды</param>
        /// <param name="canExecute">Метод разрешающий выполнение команды</param>
        public RelayCommand(ExecuteHandler execute, CanExecuteHandler canExecute = null)
        {
            _onExecute = execute ?? throw new ArgumentNullException(nameof(execute));

            if (canExecute != null)
                _canExecute = canExecute;

            invalidate = () => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            _requerySuggested = (o, e) => invalidate();
            CommandManager.RequerySuggested += _requerySuggested;
        }

        protected static readonly Dispatcher Dispatcher = Application.Current.Dispatcher;
        private readonly Action invalidate;
        /// <summary>Метод вызывающий событие для перепрове рки состояния</summary>
        public void Invalidate()
        {
            if (Dispatcher.CheckAccess())
                invalidate();
            else
                Dispatcher.BeginInvoke(invalidate);
        }

        /// <summary>Вызов метода проверяющего состояние команды</summary>
        /// <param name="parameter">Параметр команды</param>
        /// <returns><see langword="true"/> - если выполнение команды разрешено</returns>
        public bool CanExecute(object parameter) => _canExecute.Invoke(parameter);

        /// <summary>Вызов исполняющего метода команды</summary>
        /// <param name="parameter">Параметр команды</param>
        public void Execute(object parameter) => _onExecute.Invoke(parameter);
    }
    #endregion
}
