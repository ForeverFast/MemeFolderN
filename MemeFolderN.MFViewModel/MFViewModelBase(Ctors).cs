using MemeFolderN.MFViewModels.Common.Abstractions;
using MemeFolderN.MFViewModels.Common.BaseViewModels;
using MvvmNavigation.Abstractions;

namespace MemeFolderN.MFViewModels.Common
{
    public abstract partial class MFViewModelBase : BaseWindowViewModel, IMFViewModel
    {
        protected MFViewModelBase(INavigationManager navigationManager) : base(navigationManager)
        {
            
        }
    }
}
