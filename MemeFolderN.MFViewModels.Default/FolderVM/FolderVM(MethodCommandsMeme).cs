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
                        Memes.Add(new MemeVM(_navigationService, dialogService, model, dispatcher, meme));

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
            base.MemeAddMethod();
            MemeAddMethodAsync();
        }

        protected virtual async void MemeAddMethodAsync()
        {
            try
            {
                MemeDTO notSavedMemeDTO = await dialogService.MemeDtoOpenAddDialog(this.ParentFolderId);
                if (notSavedMemeDTO != null)
                    await model.AddMemeAsync(notSavedMemeDTO);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }
    }
}
