using JetBrains.Annotations;

namespace MvvmNavigation
{
    public interface IDataStorage
    {
        void Add([NotNull] string key, [NotNull] NavigationData navigationData);

        bool IsExist([NotNull] string key);

        NavigationData Get([NotNull] string key);

        bool Remove(string key);
    }
}
