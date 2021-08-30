using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Collections.Generic;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class FolderVM : FolderVMBase
    {
        protected override void FolderLoadMethod()
        {
            base.FolderLoadMethod();
            FolderLoadMethodAsync();
        }  

        protected async void FolderLoadMethodAsync()
        {
            try
            {
                if (IsFoldersLoaded)
                    return;

                IEnumerable<FolderDTO> folders = await model.GetFoldersByFolderIdAsync(this.Id);
                lock (Folders)
                {
                    foreach (FolderDTO folder in folders)
                        Folders.Add(new FolderVM(_navigationService, dialogService, model, dispatcher, folder));
                    IsBusy = false;
                    IsLoaded = (IsFoldersLoaded = true) && IsMemesLoaded;
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }


        protected override void FolderAddMethod()
        {
            base.FolderAddMethod();
            FolderAddMethodAsync();
        }

        protected virtual async void FolderAddMethodAsync()
        {
            try
            {
                FolderDTO notSavedFolderDTO = await dialogService.FolderDtoOpenAddDialog(this.ParentFolderId);
                if (notSavedFolderDTO != null)
                    await model.AddFolderAsync(notSavedFolderDTO);
            }
            catch(Exception ex)
            {
                OnException(ex);
            }
        }


        protected override void FolderChangeMethod(FolderVMBase folderVMBase)
        {
            base.FolderChangeMethod(folderVMBase);
            FolderChangeMethodAsync(folderVMBase);
        }

        protected virtual async void FolderChangeMethodAsync(FolderVMBase folderVMBase)
        {
            try
            {
                FolderDTO notSavedEditedFolderDTO = await dialogService.FolderDtoOpenEditDialog(folderVMBase.CopyDTO());
                if (notSavedEditedFolderDTO != null)
                    await model.ChangeFolderAsync(notSavedEditedFolderDTO);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }


        protected override void FolderDeleteMethod(FolderVMBase folderVMBase)
        {
            base.FolderDeleteMethod(folderVMBase);
            FolderDeleteMethodAsync(folderVMBase);
        }

        protected virtual async void FolderDeleteMethodAsync(FolderVMBase folderVMBase)
        {
            try { await model.DeleteFolderAsync(folderVMBase.CopyDTO()); }
            catch (Exception ex) { OnException(ex); }
        }


       
    }
}
