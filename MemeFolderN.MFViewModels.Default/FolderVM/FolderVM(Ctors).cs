using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFModelBase.Extentions;
using MemeFolderN.MFViewModels.Default.Extentions;
using MemeFolderN.MFViewModels.Default.MethodCommands;
using MemeFolderN.MFViewModels.Default.Services;
using MemeFolderN.MFViewModelsBase;
using MemeFolderN.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class FolderVM : FolderVMBase
    {
        private readonly VmDIContainer vmDIContainer;

        /// <summary>Диспетчер UI потока</summary>
        private readonly Dispatcher dispatcher;
        private readonly IDialogService dialogService;
        private readonly IFolderMethodCommandsClass folderMethodCommandsClass;
        private readonly IMemeMethodCommandsClass memeMethodCommandsClass;
        private readonly INavCommandsClass navCommandsClass;

        public FolderVM(VmDIContainer vmDIContainer) : base(vmDIContainer.navigationService, vmDIContainer.model)
        {
            this.vmDIContainer = vmDIContainer;

            this.dispatcher = vmDIContainer.dispatcher;
            this.dialogService = vmDIContainer.dialogService;
            this.folderMethodCommandsClass = vmDIContainer.folderMethodCommandsClass;
            this.memeMethodCommandsClass = vmDIContainer.memeMethodCommandsClass;
            this.navCommandsClass = vmDIContainer.navCommandsClass;

            model.ChangedFoldersEvent += Model_ChangedFoldersEvent;
            model.ChangedMemesEvent += Model_ChangedMemesEvent;
        }

        public FolderVM(VmDIContainer vmDIContainer, FolderDTO folderDTO) : this(vmDIContainer)
        {
            this.CopyFromDTO(folderDTO);
        }

        public override void CopyFromDTO(FolderDTO dto)
        {
            base.CopyFromDTO(dto);

            if (IsFoldersLoaded)
                return;

            if (dto.Folders?.Count > 0)
            {
                lock (Folders)
                    foreach (FolderDTO folder in dto.Folders)
                        Folders.Add(new FolderVM(vmDIContainer, folder));
                IsLoaded = (IsFoldersLoaded = true) && IsMemesLoaded;
            }


            if (IsMemesLoaded)
                return;

            if (dto.Memes?.Count > 0)
            {
                lock (Memes)
                {
                    foreach (MemeDTO meme in dto.Memes)
                        Memes.Add(new MemeVM(vmDIContainer, meme));

                    IsBusy = false;
                    IsLoaded = (IsMemesLoaded = true) && IsFoldersLoaded;
                }
            } 
        }

        public override void Dispose()
        {
            
        }
    }
}
