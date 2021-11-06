using MvvmNavigation.Abstractions;
using System;
using System.Collections.Generic;

namespace MvvmNavigation
{
    public abstract partial class NavigationManagerBase : INavigationManager
    {
        #region Fields

        private readonly object _frameControl;
        private readonly IViewInteractionStrategy _viewInteractionStrategy;
        private readonly IDataStorage _dataStorage;

        private LinkedList<string> _history;
        private LinkedListNode<string> _currentPageKey;
        private Dictionary<string, Type> PageData = new Dictionary<string, Type>();

        #endregion
    }
}
