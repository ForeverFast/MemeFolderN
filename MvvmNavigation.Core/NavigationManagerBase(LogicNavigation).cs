using MvvmNavigation.Abstractions;
using MvvmNavigation.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmNavigation
{
    public abstract partial class NavigationManagerBase : INavigationManager
    {
        #region Main Logic

        public bool CanNavigate(string navigationKey)
        {
            return _dataStorage.IsExist(navigationKey);
        }

        public void Navigate(string navigationKey, object arg)
        {
            if (navigationKey == null)
                throw new ArgumentNullException(nameof(navigationKey));

            var isKeyRegistered = CanNavigate(navigationKey);
            if (!isKeyRegistered)
                throw new InvalidOperationException(ExceptionMessages.KeyIsNotRegistered(navigationKey));

            InvokeInDispatcher(() =>
            {
                InvokeNavigatedFrom();
                var viewModel = GetViewModel(navigationKey);

                var view = CreateView(navigationKey, viewModel);
                _viewInteractionStrategy.SetContent(_frameControl, view);
                InvokeNavigatedTo(viewModel, arg);

                var navigationEventArgs = new NavigationEventArgs(view, viewModel, navigationKey, arg);
                RaiseNavigated(navigationEventArgs);
            });
        }

        private void InvokeInDispatcher(Action action)
        {
            _viewInteractionStrategy.InvokeInUIThread(_frameControl, action);
        }

        private object CreateView(string navigationKey, object viewModel)
        {
            var navigationData = _dataStorage.Get(navigationKey);
            var view = navigationData.View;
            if (view != null)
            {
                _viewInteractionStrategy.SetDataContext(view, viewModel);
            }

            return view;
        }

        private object GetViewModel(string navigationKey)
        {
            var navigationData = _dataStorage.Get(navigationKey);
            return navigationData.ViewModel;
        }

        private void InvokeNavigatedFrom()
        {
            var oldView = _viewInteractionStrategy.GetContent(_frameControl);
            if (oldView != null)
            {
                var oldViewModel = _viewInteractionStrategy.GetDataContext(oldView);
                var navigationAware = oldViewModel as INavigatingFromAware;
                navigationAware?.OnNavigatingFrom();
            }
        }

        private static void InvokeNavigatedTo(object viewModel, object arg)
        {
            var navigationAware = viewModel as INavigatedToAware;
            navigationAware?.OnNavigatedTo(arg);
        }

        #endregion

        #region Back / Forward Navigation

        public bool CanGoBack()
           => !string.IsNullOrEmpty(_currentPageKey?.Previous?.Value);

        public void GoBack()
            => Navigate(_currentPageKey.Previous.Value, NavigationType.Back, null);

        public bool CanGoForward()
            => !string.IsNullOrEmpty(_currentPageKey?.Next?.Value);

        public void GoForward()
            => Navigate(_currentPageKey.Next.Value, NavigationType.Forward, null);



        #endregion

        #region Additional Navigation Logic

        public void Navigate(string navigationKey, NavigationType navigateType, object arg = null)
        {
            if (navigationKey == null)
                throw new ArgumentNullException(nameof(navigationKey));

            var isKeyRegistered = CanNavigate(navigationKey);
            if (!isKeyRegistered)
                throw new InvalidOperationException(ExceptionMessages.KeyIsNotRegistered(navigationKey));

            LinkedListNode<string> newCurrentPage = null;

            switch (navigateType)
            {
                case NavigationType.Root:
                    _history.Clear();
                    newCurrentPage = _history.AddLast(navigationKey);
                    break;
                case NavigationType.Back:
                    newCurrentPage = _currentPageKey.Previous;
                    break;
                case NavigationType.Forward:
                    newCurrentPage = _currentPageKey.Next;
                    break;


                case NavigationType.Default:

                    if (navigationKey != _currentPageKey.Value)
                    {
                        if (!string.IsNullOrEmpty(_currentPageKey.Next?.Value))
                        {
                            var node = _currentPageKey.Next;
                            while (node != null)
                            {
                                var next = node.Next;
                                _history.Remove(node);
                                node = next;
                            }
                        }

                        newCurrentPage = _history.AddLast(navigationKey);
                    }
                    else
                        return;

                    break;



            }
            _currentPageKey = newCurrentPage;

            Navigate(navigationKey, arg);
        }

        public void Navigate<TView>(object viewModel, object arg = null)
          where TView : class, new()
        {
            var tempKey = Guid.NewGuid().ToString();
            this.Register<TView>(tempKey, viewModel);
            this.Navigate(tempKey, NavigationType.Default, arg);
        }

        public void Navigate<TView>(string navigationKey, object viewModel, object arg = null)
          where TView : class, new()
        {
            if (!CanNavigate(navigationKey))
                this.Register<TView>(navigationKey, viewModel);
            this.Navigate(navigationKey, NavigationType.Default, arg);
        }

        public void NavigateByViewTypeKey(object viewModel, string viewTypeKey, object arg = null)
        {
            var tempKey = Guid.NewGuid().ToString();
            this.RegisterWithViewTypeKey(tempKey, viewTypeKey, viewModel);
            this.Navigate(tempKey, NavigationType.Default, arg);
        }

        public void NavigateByViewTypeKey(string navigationKey, string viewTypeKey, object viewModel, object arg = null)
        {
            if (!CanNavigate(navigationKey))
                this.RegisterWithViewTypeKey(navigationKey, viewTypeKey, viewModel);
            this.Navigate(navigationKey, NavigationType.Default, arg);
        }

        #endregion

        public bool RemoveDataByKey(string key)
        {
            try
            {
                key = key ?? throw new ArgumentNullException(nameof(key));
                NavigationData nd = _dataStorage.Get(key);
                if (nd != null)
                {

                    object vm = nd.ViewModel;
                    if (vm is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                    nd.View = null;

                    if (_dataStorage.Remove(key))
                    {
                        var lsN = _history.Find(key);
                        string lsNkey = lsN.Value;
                        string currentKey = _currentPageKey.Value;
                        _history.Remove(lsN);
                        if (lsNkey == currentKey)
                        {
                            this.GoToLast();
                        }

                        GC.WaitForPendingFinalizers();
                        GC.Collect();

                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                var t = ex.Message;
                return false;
            }
        }

        private void GoToLast()
        {
            this.Navigate(_history.Last.Value, NavigationType.Default);
        }
    }
}
