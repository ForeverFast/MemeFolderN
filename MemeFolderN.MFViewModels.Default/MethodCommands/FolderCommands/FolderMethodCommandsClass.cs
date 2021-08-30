using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFViewModels.Default.Services;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Default.MethodCommands
{
    public class FolderMethodCommandsClass : IFolderMethodCommandsClass
    {
        private readonly IDialogService dialogService;
        private readonly IMFModel model;

        public virtual async void FolderAddMethodAsync(Guid? parentFolderId)
        {
            FolderDTO notSavedFolderDTO = await dialogService.FolderDtoOpenAddDialog(parentFolderId);
            if (notSavedFolderDTO != null)
                await model.AddFolderAsync(notSavedFolderDTO);
        }

        public virtual async void FolderAddNonParametersMethodAsync(Guid? parentFolderId)
        {
            await model.AddFolderAsync(new FolderDTO { ParentFolderId = parentFolderId });
        }

        public virtual async void FolderChangeMethodAsync(FolderVMBase folderVMBase)
        {
            FolderDTO notSavedEditedFolderDTO = await dialogService.FolderDtoOpenEditDialog(folderVMBase.CopyDTO());
            if (notSavedEditedFolderDTO != null)
                await model.ChangeFolderAsync(notSavedEditedFolderDTO);
        }

        public virtual async void FolderDeleteMethodAsync(FolderVMBase folderVMBase)
        {
            await model.DeleteFolderAsync(folderVMBase.CopyDTO());
        }

      
    }
}
