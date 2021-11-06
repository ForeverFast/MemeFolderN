using System;
using System.Linq;

namespace MvvmNavigation.Internal
{
    internal class NavigationManagerTypeProvider : INavigationManagerTypeProvider
    {
        private static readonly string[] AssemblyNames =
        {
            "MvvmNavigation.Wpf"
        };

        public Type GetNavigationManagerType()
        {
            return AssemblyNames.Select(assemblyName => $"MvvmNavigation.NavigationManager, {assemblyName}")
                                .Select(Type.GetType)
                                .FirstOrDefault(x => x != null);
        }
    }
}
