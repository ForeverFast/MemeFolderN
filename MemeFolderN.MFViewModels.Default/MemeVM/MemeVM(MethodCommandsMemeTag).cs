using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Collections.Generic;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MemeVM : MemeVMBase
    {
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
