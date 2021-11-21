using JetBrains.Annotations;
using MvvmNavigation.Abstractions;
using MvvmNavigation.Internal;
using System;

namespace MvvmNavigation
{
    public abstract partial class NavigationManagerBase : INavigationManager
    {
        public void Register([NotNull] string navigationKey, object getViewModel, [NotNull] object getView)
        {
            if (navigationKey == null)
                throw new ArgumentNullException(nameof(navigationKey));

            //if (getViewModel == null)
            //    throw new ArgumentNullException(nameof(getViewModel));

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
            this.Register(navigationKey, viewModel, viewInstance);
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
            this.Register(navigationKey, viewModel, viewInstance);
        }
    }
}
