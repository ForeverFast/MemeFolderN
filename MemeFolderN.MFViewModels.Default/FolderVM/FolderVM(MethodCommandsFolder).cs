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
                        Folders.Add(new FolderVM(vmDIContainer, folder));
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
            try
            {
                base.FolderAddMethod();
                folderMethodCommandsClass.FolderAddMethodAsync(this.Id);
            }
            catch (Exception ex)
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
                folderMethodCommandsClass.FolderAddNonParametersMethodAsync(this.Id);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected override void FolderChangeMethod(FolderVMBase folderVMBase)
        {
            try
            {
                base.FolderChangeMethod(folderVMBase);
                folderMethodCommandsClass.FolderChangeMethodAsync(folderVMBase);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected override void FolderDeleteMethod(FolderVMBase folderVMBase)
        {
            try
            {
                base.FolderDeleteMethod(folderVMBase);
                folderMethodCommandsClass.FolderDeleteMethodAsync(folderVMBase);
            }
            catch (Exception ex)
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
