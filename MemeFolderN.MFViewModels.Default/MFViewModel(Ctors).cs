using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFViewModels.Default.Services;
using MemeFolderN.MFViewModelsBase;
using MemeFolderN.Navigation;
using System;
using System.Windows.Threading;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MFViewModel : MFViewModelBase
    {
        /// <summary>Диспетчер UI потока</summary>
        private readonly Dispatcher dispatcher;
        private readonly IDialogService dialogService;

        public MFViewModel(INavigationService navigationService,
            IDialogService dialogService,
            IMFModel model,
            Dispatcher dispatcher) : base(navigationService, model)
        {
            this.dispatcher = dispatcher;
            this.dialogService = dialogService;

            model.ChangedFoldersEvent += Model_ChangedFoldersEvent;
            model.ChangedMemeTagsEvent += Model_ChangedMemeTagsEvent;
        }

       

        public override void Dispose()
        {
            
        }
    }
}
