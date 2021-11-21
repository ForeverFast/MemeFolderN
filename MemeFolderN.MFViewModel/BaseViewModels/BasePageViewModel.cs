

using MvvmNavigation.Abstractions;

namespace MemeFolderN.MFViewModels.Common.BaseViewModels
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
