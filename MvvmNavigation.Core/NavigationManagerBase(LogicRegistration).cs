using JetBrains.Annotations;
using MvvmNavigation.Abstractions;
using MvvmNavigation.Internal;
using System;

namespace MvvmNavigation
{
    public abstract partial class NavigationManagerBase : INavigationManager
    {
        public void Register([NotNull] string navigationKey, [NotNull] Func<object> getViewModel, [NotNull] Func<object> getView)
        {
            if (navigationKey == null)
                throw new ArgumentNullException(nameof(navigationKey));

            if (getViewModel == null)
                throw new ArgumentNullException(nameof(getViewModel));

            if (getView == null)
                throw new ArgumentNullException(nameof(getView));

            var isKeyAlreadyRegistered = _dataStorage.IsExist(navigationKey);
            if (isKeyAlreadyRegistered)
                throw new InvalidOperationException(ExceptionMessages.CanNotRegisterKeyTwice);

            var navigationData = new NavigationData(getViewModel, getView);
            _dataStorage.Add(navigationKey, navigationData);
        }

        public void Register<TView>([NotNull] string navigationKey, object viewModel)
          where TView : class, new()
        {
            object viewInstance = Activator.CreateInstance<TView>();
            Func<object> getView = () => viewInstance;
            this.Register(navigationKey, () => viewModel, getView);
        }

        public void RegisterViewType<TView>([NotNull] string viewTypeKey)
             where TView : class, new()
        {
            if (viewTypeKey == null)
                throw new ArgumentNullException(nameof(viewTypeKey));

            PageData.Add(viewTypeKey, typeof(TView));
        }

        public void RegisterWithViewTypeKey([NotNull] string navigationKey, [NotNull] string viewTypeKey, object viewModel)
        {
            Type viewType = PageData[viewTypeKey];
            object viewInstance = Activator.CreateInstance(viewType);
            Func<object> getView = () => viewInstance;
            this.Register(navigationKey, () => viewModel, getView);
        }
    }
}
