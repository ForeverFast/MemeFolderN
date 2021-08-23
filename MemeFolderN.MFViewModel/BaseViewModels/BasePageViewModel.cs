using MemeFolderN.Navigation;

namespace MemeFolderN.MFViewModelsBase.BaseViewModels
{
    public abstract class BasePageViewModel : BaseNavigationViewModel
    {
        #region Конструкторы

        public BasePageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        #endregion
    }
}
