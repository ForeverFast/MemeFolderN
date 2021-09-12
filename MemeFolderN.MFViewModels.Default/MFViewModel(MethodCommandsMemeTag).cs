using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Collections.Generic;

namespace MemeFolderN.MFViewModels.Default
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
                if (IsMemeTagsLoaded)
                    return;

                IEnumerable<MemeTagDTO> memeTags = await model.GetAllMemeTagsAsync();
                lock (MemeTags)
                {
                    foreach (MemeTagDTO memeTag in memeTags)
                        MemeTags.Add(new MemeTagVM(memeTag));

                    IsBusy = !(IsLoaded = (IsMemeTagsLoaded = true) && IsFoldersLoaded && IsMemesLoaded);
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }

        protected override void MemeTagAddMethod()
        {
            try
            {
                base.MemeTagAddMethod();
                memeTagMethodCommandsClass.MemeTagAddMethodAsync();
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

        protected override void MemeTagChangeMethod(MemeTagVMBase memeTagVMBase)
        {
            try
            {
                base.MemeTagChangeMethod(memeTagVMBase);
                memeTagMethodCommandsClass.MemeTagChangeMethodAsync(memeTagVMBase.CopyDTO());
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

        protected override void MemeTagDeleteMethod(MemeTagVMBase memeTagVMBase)
        {
            try
            {
                base.MemeTagDeleteMethod(memeTagVMBase);
                memeTagMethodCommandsClass.MemeTagDeleteMethodAsync(memeTagVMBase.CopyDTO());
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
