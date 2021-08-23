using MemeFolderN.MFViewModelsBase.Commands;
using MemeFolderN.Navigation;
using System.Windows.Input;

namespace MemeFolderN.MFViewModelsBase.BaseViewModels
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

        public BaseWindowViewModel(INavigationService navigationService) : base(navigationService)
        {
            MinimizedWindowCommand = new RelayCommand(MinimizedWindowMethod);
            ResizeWindowCommand = new RelayCommand(ResizeWindowMethod);
            CloseWindowCommand = new RelayCommand(CloseWindowMethod);
            DragWindowCommand = new RelayCommand(DragWindowMethod);
        }
    }
}
