using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MFViewModel : MFViewModelBase
    {
        protected override void MemeTagLoadMethod()
        {
            base.MemeTagLoadMethod();
            MemeTagLoadMethodAsync();
        }

        // Отошёл

        protected virtual async void MemeTagLoadMethodAsync()
        {
            try
            {
                if (IsMemeTagsLoaded)
                    return;

                IEnumerable<MemeTagDTO> memeTags = await model.GetMemeTagsAsync();
                lock (MemeTags)
                {
                    foreach (MemeTagDTO memeTag in memeTags)
                        MemeTags.Add(new MemeTagVM(memeTag));

                    IsBusy = !(IsLoaded = (IsMemeTagsLoaded = true) && IsFoldersLoaded);
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }

        protected override void MemeTagAddMethod()
        {
            base.MemeTagAddMethod();
            MemeTagAddMethodAsync();
        }

        protected virtual async void MemeTagAddMethodAsync()
        {
            try
            {
                MemeTagDTO notSavedMemeTagDTO = await dialogService.MemeTagDtoOpenAddDialog();
                if (notSavedMemeTagDTO != null)
                    await model.AddMemeTagAsync(notSavedMemeTagDTO);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }

        protected override void MemeTagChangeMethod(MemeTagVMBase memeTagVMBase)
        {
            base.MemeTagChangeMethod(memeTagVMBase);
            MemeTagChangeMethodAsync(memeTagVMBase);
        }

        protected virtual async void MemeTagChangeMethodAsync(MemeTagVMBase memeTagVMBase)
        {
            try
            {
                MemeTagDTO notSavedEditedMemeTagDTO = await dialogService.MemeTagDtoOpenEditDialog(memeTagVMBase.CopyDTO());
                if (notSavedEditedMemeTagDTO != null)
                    await model.ChangeMemeTagAsync(notSavedEditedMemeTagDTO);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }

        protected override void MemeTagDeleteMethod(MemeTagVMBase memeTagVMBase)
        {
            base.MemeTagDeleteMethod(memeTagVMBase);
            MemeTagDeleteMethodAsync(memeTagVMBase);
        }
        protected virtual async void MemeTagDeleteMethodAsync(MemeTagVMBase memeTagVMBase)
        {
            try { await model.DeleteMemeTagAsync(memeTagVMBase.CopyDTO()); }
            catch (Exception ex) { OnException(ex); }
        }
    }
}
