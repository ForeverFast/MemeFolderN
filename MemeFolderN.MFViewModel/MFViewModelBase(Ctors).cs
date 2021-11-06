using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using MvvmNavigation.Abstractions;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class MFViewModelBase : BaseWindowViewModel, IMFViewModel
    {
        protected MFViewModelBase(INavigationManager navigationManager) : base(navigationManager)
        {
            
        }
    }
}
