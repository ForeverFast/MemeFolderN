using MemeFolderN.MFViewModelsBase.Commands;
using MemeFolderN.Navigation;
using System.Windows.Input;

namespace MemeFolderN.MFViewModelsBase.BaseViewModels
{
    public abstract class BaseNavigationViewModel : BaseViewModel
    {
        protected readonly INavigationService _navigationService;

        #region Навигация
        public ICommand NavigationToCommand { get; }
        public ICommand NavigationBackCommand { get; }
        public ICommand NavigationForwardCommand { get; }
        public ICommand NavigationToFolderCommand { get; set; }

        protected virtual void NavigationToExecute(object parameter)
            => _navigationService.Navigate(parameter.ToString(), NavigationType.Default);

        protected virtual void NavigationBackExecute()
            => _navigationService.GoBack();

        protected virtual void NavigationForwardExecute()
            => _navigationService.GoForward();

        #endregion


        #region Конструкторы 

        public BaseNavigationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigationToCommand = new RelayCommand(NavigationToExecute, (o) => _navigationService.CanNavigate(o.ToString()));
            NavigationBackCommand = new RelayCommandAction(NavigationBackExecute, () => _navigationService.CanGoBack());
            NavigationForwardCommand = new RelayCommandAction(NavigationForwardExecute, () => _navigationService.CanGoForward());
        }

        #endregion
    }
}
