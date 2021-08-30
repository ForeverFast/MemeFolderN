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
                        RootFolders.Add(new FolderVM(vmDIContainer, folder));
                    
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
            try
            {
                base.FolderAddMethod();
                folderMethodCommandsClass.FolderAddMethodAsync(null);
            }
            catch(Exception ex)
            {
                OnException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        protected override void FolderAddNonParametersMethod()
        {
            try
            {
                base.FolderAddNonParametersMethod();
                folderMethodCommandsClass.FolderAddNonParametersMethodAsync(null);
            }
            catch(Exception ex)
            {
                OnException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
