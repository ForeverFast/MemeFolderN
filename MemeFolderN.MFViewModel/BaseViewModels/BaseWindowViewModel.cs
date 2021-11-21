using MemeFolderN.MFViewModels.Common.Commands;
using MvvmNavigation.Abstractions;
using System.Windows.Input;

namespace MemeFolderN.MFViewModels.Common.BaseViewModels
{
    public abstract class BaseWindowViewModel : BaseNavigationViewModel
    {
        #region Управление окном

        public ICommand MinimizedWindowCommand { get; }
        public ICommand ResizeWindowCommand { get; }
        public ICommand CloseWindowCommand { get; }
        public ICommand DragWindowCommand { get; }

        public abstract void MinimizedWindowMethod(object parameter);

        public abstract void ResizeWindowMethod(object parameter);

        public abstract void CloseWindowMethod(object parameter);

        public abstract void DragWindowMethod(object parameter);

        #endregion

        public BaseWindowViewModel(INavigationManager navigationManager) : base(navigationManager)
        {
            MinimizedWindowCommand = new RelayCommand(MinimizedWindowMethod);
            ResizeWindowCommand = new RelayCommand(ResizeWindowMethod);
            CloseWindowCommand = new RelayCommand(CloseWindowMethod);
            DragWindowCommand = new RelayCommand(DragWindowMethod);
        }
    }
}
