using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class FolderVM : FolderVMBase
    {
        protected override void FolderFoldersMethod()
        {
            base.FolderFoldersMethod();
            FolderFoldersMethodAsync();
        }  

        protected async void FolderFoldersMethodAsync()
        {
            try
            {
                IEnumerable<FolderDTO> folders = await model.GetFoldersByFolderIdAsync(this.Id);
                lock (Folders)
                {
                    foreach (FolderDTO folder in folders)
                        Folders.Add(new FolderVM(_navigationService, dialogService, model, dispatcher, folder));
                    IsBusy = false;
                    IsLoaded = true;
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

            }
            catch(Exception ex)
            {
                OnException(ex);
            }
        }
    }
}
