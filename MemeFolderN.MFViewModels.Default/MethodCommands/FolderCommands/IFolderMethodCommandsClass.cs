using MemeFolderN.MFViewModelsBase;
using System;

namespace MemeFolderN.MFViewModels.Default.MethodCommands
{
    public interface IFolderMethodCommandsClass
    {
        void FolderAddMethodAsync(Guid? parentFolderId);
        void FolderAddNonParametersMethodAsync(Guid? parentFolderId);
        void FolderChangeMethodAsync(FolderVMBase folderVMBase);
        void FolderDeleteMethodAsync(FolderVMBase folderVMBase);
    }
}
