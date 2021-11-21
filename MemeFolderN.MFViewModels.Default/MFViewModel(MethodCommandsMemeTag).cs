using MemeFolderN.Common.DTOClasses;
using MemeFolderN.MFViewModels.Common;
using System;
using System.Collections.Generic;

namespace MemeFolderN.MFViewModels.Wpf
{
    public partial class MFViewModel : MFViewModelBase
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
                if (IsMemeTagsLoadedFlag)
                    return;

                IEnumerable<MemeTagDTO> memeTags = await model.GetAllMemeTagsAsync();
                lock (MemeTags)
                {
                    foreach (MemeTagDTO memeTag in memeTags)
                        MemeTags.Add(new MemeTagVM(memeTag));

                    IsMemeTagsLoadedFlag = true;
                    LoadCheck();
                    BusyCheck();
                }
            }
            catch (Exception ex)
            {
                IsMemeTagsLoadedFlag = true;
                BusyCheck();
                OnException(ex);
            }
        }

        protected override void MemeTagAddMethod()
        {
            base.MemeTagAddMethod();
            MemeTagAddMethodAsync();
        }

        public virtual async void MemeTagAddMethodAsync()
        {
            try
            {
                IsMemeTagsLoadedFlag = false;
                MemeTagDTO notSavedMemeTagDTO = await dialogService.MemeTagDtoOpenAddDialog();
                if (notSavedMemeTagDTO != null)
                    await model.AddMemeTagAsync(notSavedMemeTagDTO);
                else
                {
                    IsMemeTagsLoadedFlag = true;
                    BusyCheck();
                }
            }
            catch (Exception ex)
            {
                IsMemeTagsLoadedFlag = true;
                BusyCheck();
                OnException(ex);
            }
        }

        protected override void MemeTagChangeMethod(MemeTagVMBase memeTagVMBase)
        {
            base.MemeTagChangeMethod(memeTagVMBase);
            MemeTagChangeMethodAsync(memeTagVMBase.CopyDTO());
        }

        public virtual async void MemeTagChangeMethodAsync(MemeTagDTO memeTagDTO)
        {
            try
            {
                IsMemeTagsLoadedFlag = false;
                MemeTagDTO notSavedEditedMemeTagDTO = await dialogService.MemeTagDtoOpenEditDialog(memeTagDTO);
                if (notSavedEditedMemeTagDTO != null)
                    await model.ChangeMemeTagAsync(notSavedEditedMemeTagDTO);
                else
                {
                    IsMemeTagsLoadedFlag = true;
                    BusyCheck();
                }
            }
            catch (Exception ex)
            {
                IsMemeTagsLoadedFlag = true;
                BusyCheck();
                OnException(ex);
            }
        }

        protected override void MemeTagDeleteMethod(MemeTagVMBase memeTagVMBase)
        {
            base.MemeTagDeleteMethod(memeTagVMBase);
            MemeTagDeleteMethodAsync(memeTagVMBase.CopyDTO());
        }

        public virtual async void MemeTagDeleteMethodAsync(MemeTagDTO memeTagDTO)
        {
            try
            {
                IsMemeTagsLoadedFlag = false;
                await model.DeleteMemeTagAsync(memeTagDTO);
            }
            catch (Exception ex)
            {
                IsMemeTagsLoadedFlag = true;
                BusyCheck();
                OnException(ex);
            }
        }
    }
}
