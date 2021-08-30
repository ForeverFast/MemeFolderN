using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Collections.Generic;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class FolderVM : FolderVMBase
    {
        protected override void MemeLoadMethod()
        {
            base.MemeLoadMethod();
            MemeLoadMethodAsync();
        }

        protected virtual async void MemeLoadMethodAsync()
        {
            try
            {
                if (IsMemesLoaded)
                    return;

                IEnumerable<MemeDTO> memes = await model.GetMemesByFolderIdAsync(this.Id);
                lock (Memes)
                {
                    foreach (MemeDTO meme in memes)
                        Memes.Add(new MemeVM(vmDIContainer, meme));

                    IsBusy = false;
                    IsLoaded = (IsMemesLoaded = true) && IsFoldersLoaded;
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }

        protected override void MemeAddMethod()
        {
            try
            {
                base.MemeAddMethod();
                memeMethodCommandsClass.MemeAddMethodAsync(this.ParentFolderId);
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
