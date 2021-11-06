

using MvvmNavigation.Abstractions;

namespace MemeFolderN.MFViewModelsBase.BaseViewModels
{
    public abstract class BasePageViewModel : BaseNavigationViewModel
    {
        #region Конструкторы

        public BasePageViewModel(INavigationManager navigationManager) : base(navigationManager)
        {

        }

        #endregion
    }
}
