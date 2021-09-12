using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFViewModels.Default.Extentions;
using MemeFolderN.MFViewModels.Default.MethodCommands;
using MemeFolderN.MFViewModels.Default.Services;
using MemeFolderN.MFViewModelsBase;
using MemeFolderN.Navigation;
using System;
using System.Windows.Threading;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MFViewModel : MFViewModelBase
    {
        private readonly VmDIContainer vmDIContainer;

        /// <summary>Диспетчер UI потока</summary>
        private readonly Dispatcher dispatcher;
        private readonly IDialogService dialogService;
        private readonly IFolderMethodCommandsClass folderMethodCommandsClass;
        private readonly IMemeMethodCommandsClass memeMethodCommandsClass;
        private readonly IMemeTagMethodCommandsClass memeTagMethodCommandsClass;
        private readonly INavCommandsClass navCommandsClass;

        public MFViewModel(VmDIContainer vmDIContainer) : base(vmDIContainer.navigationService, vmDIContainer.model)
        {
            this.vmDIContainer = vmDIContainer;

            this.dispatcher = vmDIContainer.dispatcher;
            this.dialogService = vmDIContainer.dialogService;
            this.folderMethodCommandsClass = vmDIContainer.folderMethodCommandsClass;
            this.memeMethodCommandsClass = vmDIContainer.memeMethodCommandsClass;
            this.memeTagMethodCommandsClass = vmDIContainer.memeTagMethodCommandsClass;
            this.navCommandsClass = vmDIContainer.navCommandsClass;

            model.ChangedFoldersEvent += Model_ChangedFoldersEvent;
            model.ChangedMemeTagsEvent += Model_ChangedMemeTagsEvent;
        }

        // Только для Времени Разработки
        public MFViewModel() : base(null, null)
        { }

        public override void Dispose()
        {
            
        }
    }
}
