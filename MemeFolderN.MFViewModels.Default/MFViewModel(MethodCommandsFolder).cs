using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Collections.Generic;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MFViewModel : MFViewModelBase
    {
        protected override void FolderRootsMethod()
        {
            base.FolderRootsMethod();
            FolderRootsMethodAsync();
        }

        protected virtual async void FolderRootsMethodAsync()
        {
            try
            {
                if (IsFoldersLoaded)
                    return;

                IEnumerable<FolderDTO> folders = await model.GetRootFoldersAsync();
                lock (RootFolders)
                {
                    foreach (FolderDTO folder in folders)
                        RootFolders.Add(new FolderVM(_navigationService, dialogService, model, dispatcher, folder));
                    
                    IsBusy = !(IsLoaded = (IsFoldersLoaded = true) && IsMemeTagsLoaded);
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
                FolderDTO notSavedFolderDTO = await dialogService.FolderDtoOpenAddDialog(null);
                if (notSavedFolderDTO != null)
                    await model.AddFolderAsync(notSavedFolderDTO);
                else
                    IsBusy = false;
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }

        protected override void FolderAddNonParametersMethod()
        {
            base.FolderAddNonParametersMethod();
            FolderAddNonParametersMethodAsync();
        }

        protected virtual async void FolderAddNonParametersMethodAsync()
        {
            try { await model.AddFolderAsync(new FolderDTO { ParentFolderId = null }); }
            catch(Exception ex) { OnException(ex); }
        }
    }
}
