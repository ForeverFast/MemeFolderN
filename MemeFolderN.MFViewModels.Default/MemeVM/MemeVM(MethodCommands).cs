using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MemeVM : MemeVMBase
    {
        protected override void MemeChangeMethod(MemeVMBase memeVMBase)
        {
            base.MemeChangeMethod(memeVMBase);
            MemeChangeMethodAsync(memeVMBase);
        }

        protected virtual async void MemeChangeMethodAsync(MemeVMBase memeVMBase)
        {
            try
            {
                MemeDTO notSavedEditedMemeDTO = await dialogService.MemeDtoOpenEditDialog(this.CopyDTO());
                if (notSavedEditedMemeDTO != null)
                    await model.ChangeMemeAsync(notSavedEditedMemeDTO);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }

        protected override void MemeOpenMethod()
        {
            base.MemeOpenMethod();
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(this.ImagePath) { UseShellExecute = true };
            p.Start();
            p.Dispose();
        }

        protected override void MemeCopyMethod()
        {
            base.MemeCopyMethod();
            Clipboard.SetImage(new BitmapImage(new Uri(this.ImagePath)));
        }
        
        protected override void MemeTagLoadMethod()
        {
            base.MemeTagLoadMethod();
            MemeTagLoadMethodAsync();
        }

        protected virtual async void MemeTagLoadMethodAsync()
        {
            try
            {
                if (IsMemeTagsLoaded)
                    return;

                IEnumerable<MemeTagDTO> memeTags = await model.GetMemeTagsByMemeIdAsync(this.Id);
                lock (Tags)
                {
                    foreach (MemeTagDTO memeTag in memeTags)
                        Tags.Add(new MemeTagVM(memeTag));

                    IsBusy = false;
                    IsLoaded = IsMemeTagsLoaded = true;
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }
    }
}
