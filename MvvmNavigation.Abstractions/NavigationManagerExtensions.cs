using JetBrains.Annotations;

namespace MvvmNavigation.Abstractions
{
    public static class NavigationManagerExtensions
    {
        public static void Navigate(this INavigationManager navigationManager, [NotNull] string navigationKey)
        {
            navigationManager.Navigate(navigationKey, null);
        }

        public static void Navigate(this INavigationManager navigationService, [NotNull] string navigationKey, NavigationType navigateType)
        {
            navigationService.Navigate(navigationKey, navigateType, null);
        }
    }
}
