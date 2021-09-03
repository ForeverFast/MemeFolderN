using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Internal;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MemeFolderN.Navigation
{
    public class NavigationService : NavigationManager, INavigationService
    {
        private LinkedList<string> _history;
        private LinkedListNode<string> _currentPageKey;
        private Dictionary<string, Type> PageData = new Dictionary<string, Type>();


        public NavigationService([NotNull] ContentControl frameControl) : base(frameControl)
        {
            _history = new LinkedList<string>();      
        }

        #region Навигация

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

            base.Navigate(navigationKey, arg);
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


        #region Навигация Вперёд / Назад

        public bool CanGoBack()
           => !string.IsNullOrEmpty(_currentPageKey?.Previous?.Value);

        public void GoBack()
            => Navigate(_currentPageKey.Previous.Value, NavigationType.Back, null);

        public bool CanGoForward()
            => !string.IsNullOrEmpty(_currentPageKey?.Next?.Value);

        public void GoForward()
            => Navigate(_currentPageKey.Next.Value, NavigationType.Forward, null);



        #endregion

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
