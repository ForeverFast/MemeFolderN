using MemeFolderN.MFViewModelsBase.Commands;
using MemeFolderN.Navigation;

namespace MemeFolderN.MFViewModelsBase.BaseViewModels
{
    public abstract class BaseNavigationViewModel : BaseViewModel
    {
        protected readonly INavigationService navigationService;

        public RelayCommand NavigationToCommand => _navigationToCommand ?? (_navigationToCommand =
            new RelayCommandAction<string>(NavigationToExecute));

        protected virtual void NavigationToExecute(string parameter)
        {
#if DEBUG
            ShowMetod($"Вызван метод стандартной навигации по ключу. Ключ: {parameter}.");
#endif
        }

        public RelayCommand NavigationBackCommand => _navigationBackCommand ?? (_navigationBackCommand =
            new RelayCommandAction(NavigationBackMethod, () => navigationService.CanGoBack()));

        protected virtual void NavigationBackMethod()
        {
#if DEBUG
            ShowMetod($"Вызван метод навигации к предыдущей странице.");
#endif
        }

        public RelayCommand NavigationForwardCommand => _navigationForwardCommand ?? (_navigationForwardCommand =
            new RelayCommandAction(NavigationForwardMethod, () => navigationService.CanGoForward()));

        protected virtual void NavigationForwardMethod()
        {
#if DEBUG
            ShowMetod($"Вызван метод навигации к следующей странице.");
#endif
        }

        #region Поля для хранения значений свойств
        private RelayCommand _navigationToCommand;
        private RelayCommand _navigationBackCommand;
        private RelayCommand _navigationForwardCommand;
        #endregion

        #region Конструкторы 

        public BaseNavigationViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        #endregion
    }
}
