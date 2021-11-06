using System;
using JetBrains.Annotations;

namespace MvvmNavigation.Abstractions
{
    public interface INavigationManager
    {
        event EventHandler<NavigationEventArgs> Navigated;

        bool CanNavigate(string navigationKey);
        void Navigate([NotNull] string navigationKey, object arg);
        void Navigate(string navigationKey, NavigationType navigateType, object arg = null);
        void Navigate<TView>(object viewModel, object arg) where TView : class, new();
        void Navigate<TView>(string navigationKey, object viewModel, object arg) where TView : class, new();
        void NavigateByViewTypeKey(object viewModel, string viewTypeKey, object arg = null);
        void NavigateByViewTypeKey(string navigationKey, string viewTypeKey, object viewModel, object arg = null);

        void Register<TView>([NotNull] string navigationKey, object viewModel) where TView : class, new();
        void RegisterViewType<TView>([NotNull] string viewTypeKey) where TView : class, new();
        void RegisterWithViewTypeKey([NotNull] string navigationKey, [NotNull] string viewTypeKey, object viewModel);

        bool CanGoBack();
        void GoBack();
        bool CanGoForward();
        void GoForward();

        bool RemoveDataByKey(string key);
    }
}
