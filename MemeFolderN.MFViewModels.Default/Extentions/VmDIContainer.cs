using MemeFolderN.MFModel.Wpf.Abstractions;
using MemeFolderN.MFViewModels.Default.Services;
using MvvmNavigation.Abstractions;
using System.Windows.Threading;

namespace MemeFolderN.MFViewModels.Default.Extentions
{
    public class VmDIContainer
    {
        public readonly Dispatcher dispatcher;
        public readonly IMFModelWpf model;
        public readonly INavigationManager navigationManager;
        public readonly IDialogService dialogService;

        public VmDIContainer(INavigationManager navigationManager,
            IDialogService dialogService,
            IMFModelWpf model,
            Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            this.model = model;
            this.navigationManager = navigationManager;
            this.dialogService = dialogService;
        }
    }
}
