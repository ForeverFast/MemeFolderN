using MemeFolderN.MFViewModelsBase;
using System;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MemeVM : MemeVMBase
    {
        protected override void MemeChangeMethod(MemeVMBase memeVMBase)
        {
            try
            {
                base.MemeChangeMethod(memeVMBase);
                memeMethodCommandsClass.MemeChangeMethodAsync(memeVMBase.CopyDTO());
            }
            catch(Exception ex)
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

        protected override void MemeOpenMethod()
        {
            try
            {
                base.MemeOpenMethod();
                memeMethodCommandsClass.MemeOpenMethod(this.ImagePath);
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

        protected override void MemeCopyMethod()
        {
            try
            {
                base.MemeCopyMethod();
                memeMethodCommandsClass.MemeCopyMethod(this.ImagePath);
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
