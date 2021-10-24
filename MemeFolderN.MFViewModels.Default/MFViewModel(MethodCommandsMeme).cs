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

                IEnumerable<MemeDTO> memes = await model.GetAllMemesAsync();
                lock (Memes)
                {
                    foreach (MemeDTO meme in memes)
                        Memes.Add(new MemeVM(meme));

                    IsBusy = !(IsLoaded = (IsMemesLoaded = true) && IsFoldersLoaded && IsMemeTagsLoaded);
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }

        protected override void MemeAddNonParametersMethod(FolderVMBase folderVMBase)
        {
            
            try
            {
                base.MemeAddNonParametersMethod(folderVMBase);
                memeMethodCommandsClass.MemeAddNonParametersMethodAsync(folderVMBase.Id);
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

        protected override void MemeAddMethod(FolderVMBase folderVMBase)
        {
            try
            {
                base.MemeAddMethod(folderVMBase);
                memeMethodCommandsClass.MemeAddMethodAsync(folderVMBase.Id);
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

        protected override void MemeChangeMethod(MemeVMBase memeVMBase)
        {
            try
            {
                base.MemeChangeMethod(memeVMBase);
                memeMethodCommandsClass.MemeChangeMethodAsync(memeVMBase.CopyDTO());
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

        protected override void MemeDeleteMethod(MemeVMBase memeVMBase)
        {
            try
            {
                base.MemeDeleteMethod(memeVMBase);
                memeMethodCommandsClass.MemeDeleteMethodAsync(memeVMBase.CopyDTO());
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

        protected override void MemeOpenMethod(MemeVMBase memeVMBase)
        {
            try
            {
                base.MemeOpenMethod(memeVMBase);
                memeMethodCommandsClass.MemeOpenMethod(memeVMBase.ImagePath);
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

        protected override void MemeCopyMethod(MemeVMBase memeVMBase)
        {
            try
            {
                base.MemeCopyMethod(memeVMBase);
                memeMethodCommandsClass.MemeCopyMethod(memeVMBase.ImagePath);
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
