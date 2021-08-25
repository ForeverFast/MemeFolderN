using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using MemeFolderN.Navigation;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class MFViewModelBase : BaseWindowViewModel, IMFViewModel
    {
        protected MFViewModelBase(INavigationService navigationService,
            IMFModel model) : base(navigationService)
        {
            this.model = model;
        }
    }
}
