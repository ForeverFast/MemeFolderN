using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Collections.Generic;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MFViewModel : MFViewModelBase
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
                if (IsFoldersLoadedFlag)
                    return;

                List<FolderDTO> folders = await model.GetAllFoldersAsync();
                lock (Folders)
                {
                    folders.ForEach(f => Folders.Add(new FolderVM(f)));

                    IsFoldersLoadedFlag = true;
                    LoadCheck();
                    BusyCheck();
                }
            }
            catch (Exception ex)
            {
                IsFoldersLoadedFlag = true;
                BusyCheck();
                OnException(ex);
            }
        }

        protected override void FolderAddMethod(FolderVMBase folderVMBase)
        {
            base.FolderAddMethod(folderVMBase);
            FolderAddMethodAsync(folderVMBase?.Id);
        }

        public async void FolderAddMethodAsync(Guid? parentFolderId)
        {
            try
            {
                IsFoldersLoadedFlag = false;
                FolderDTO notSavedFolderDTO = await dialogService.FolderDtoOpenAddDialog(parentFolderId);
                if (notSavedFolderDTO != null)
                    await model.AddFolderAsync(notSavedFolderDTO);
                else
                {
                    IsFoldersLoadedFlag = true;
                    BusyCheck();
                }
            }
            catch (Exception ex)
            {
                IsFoldersLoadedFlag = true;
                BusyCheck();
                OnException(ex);
            }
        }

        protected override void FolderAddNonParametersMethod(FolderVMBase folderVMBase)
        {
            base.FolderAddNonParametersMethod(folderVMBase);
            FolderAddNonParametersMethodAsync(folderVMBase?.Id);
        }

        public async void FolderAddNonParametersMethodAsync(Guid? parentFolderId)
        {
            try
            {
                IsFoldersLoadedFlag = false;
                await model.AddFolderAsync(new FolderDTO { ParentFolderId = parentFolderId });
            }
            catch (Exception ex)
            {
                IsFoldersLoadedFlag = true;
                BusyCheck();
                OnException(ex);
            }
        }

        protected override void FolderChangeMethod(FolderVMBase folderVMBase)
        {
            base.FolderChangeMethod(folderVMBase);
            FolderChangeMethodAsync(folderVMBase);
        }

        public async void FolderChangeMethodAsync(FolderVMBase folderVMBase)
        {
            try
            {
                IsFoldersLoadedFlag = false;
                FolderDTO notSavedEditedFolderDTO = await dialogService.FolderDtoOpenEditDialog(folderVMBase.CopyDTO());
                if (notSavedEditedFolderDTO != null)
                    await model.ChangeFolderAsync(notSavedEditedFolderDTO);
                else
                {
                    IsFoldersLoadedFlag = true;
                    BusyCheck();
                }
            }
            catch (Exception ex)
            {
                IsFoldersLoadedFlag = true;
                BusyCheck();
                OnException(ex);
            }
        }

        protected override void FolderDeleteMethod(FolderVMBase folderVMBase)
        {
            base.FolderDeleteMethod(folderVMBase);
            FolderDeleteMethodAsync(folderVMBase);
        }

        public virtual async void FolderDeleteMethodAsync(FolderVMBase folderVMBase)
        {
            try
            {
                IsFoldersLoadedFlag = false;
                IsMemesLoadedFlag = false;
                await model.DeleteFolderAsync(folderVMBase.CopyDTO());
            }
            catch (Exception ex)
            {
                IsMemesLoadedFlag = true;
                IsFoldersLoadedFlag = true;
                BusyCheck();
                OnException(ex);
            }
        }
    }
}
