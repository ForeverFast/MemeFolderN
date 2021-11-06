using System;
using MvvmNavigation.Abstractions;
using MvvmNavigation.Internal;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace MvvmNavigation
{
    public abstract partial class NavigationManagerBase : INavigationManager
    {
        #region Ctor

        protected NavigationManagerBase([NotNull] object frameControl, IViewInteractionStrategy viewInteractionStrategy)
            : this(frameControl, viewInteractionStrategy, new DataStorage())
        {
            _history = new LinkedList<string>();
        }

        protected NavigationManagerBase([NotNull] object frameControl,
                                        IViewInteractionStrategy viewInteractionStrategy,
                                        [NotNull] IDataStorage dataStorage)
        {
            _frameControl = frameControl ?? throw new ArgumentNullException(nameof(frameControl));
            _viewInteractionStrategy = viewInteractionStrategy ?? throw new ArgumentNullException(nameof(viewInteractionStrategy));
            _dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
            _history = new LinkedList<string>();
        }

        #endregion

        #region Navigated

        public event EventHandler<NavigationEventArgs> Navigated;

        private void RaiseNavigated(NavigationEventArgs e)
        {
            Navigated?.Invoke(this, e);
        }

        #endregion
    }
}
