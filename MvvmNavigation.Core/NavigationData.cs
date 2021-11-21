using System;
using JetBrains.Annotations;

namespace MvvmNavigation
{
    public sealed class NavigationData
    {
        public NavigationData(object viewModelFunc, [NotNull] object viewFunc)
        {
            ViewModel = viewModelFunc;
            View = viewFunc ?? throw new ArgumentNullException(nameof(viewFunc));
        }

        public object ViewModel { get; set; }

        public object View { get; set; }
    }
}
