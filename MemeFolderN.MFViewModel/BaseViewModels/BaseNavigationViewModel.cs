using MemeFolderN.MFViewModels.Common.Commands;
using MvvmNavigation.Abstractions;

namespace MemeFolderN.MFViewModels.Common.BaseViewModels
{
    public abstract class BaseNavigationViewModel : BaseViewModel
    {
        protected readonly INavigationManager navigationManager;

        public RelayCommand NavigationToCommand => _navigationToCommand ?? (_navigationToCommand =
            new RelayCommandAction<string>(NavigationToExecute));

        protected virtual void NavigationToExecute(string parameter)
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызван метод стандартной навигации по ключу. Ключ: {parameter}.");
#endif
        }

        public RelayCommand NavigationBackCommand => _navigationBackCommand ?? (_navigationBackCommand =
            new RelayCommandAction(NavigationBackMethod, () => navigationManager.CanGoBack()));

        protected virtual void NavigationBackMethod()
        {
            IsBusy = true;
#if DEBUG
            ShowMetod($"Вызван метод навигации к предыдущей странице.");
#endif
        }

        public RelayCommand NavigationForwardCommand => _navigationForwardCommand ?? (_navigationForwardCommand =
            new RelayCommandAction(NavigationForwardMethod, () => navigationManager.CanGoForward()));

        protected virtual void NavigationForwardMethod()
        {
            IsBusy = true;
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

        public BaseNavigationViewModel(INavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }

        #endregion
    }
}
