using MemeFolderN.Common.DTOClasses;
using MemeFolderN.Common.Helpers;
using MemeFolderN.MFViewModels.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MemeFolderN.MFViewModels.Wpf
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
                if (IsMemesLoadedFlag)
                    return;

                IEnumerable<MemeDTO> memes = await model.GetAllMemesAsync();
                lock (Memes)
                {
                    foreach (MemeDTO meme in memes)
                        Memes.Add(new MemeVM(meme));

                    IsMemesLoadedFlag = true;
                    LoadCheck();
                    BusyCheck();
                }
            }
            catch (Exception ex)
            {
                IsMemesLoadedFlag = true;
                BusyCheck();
                OnException(ex);
            }
        }

        protected override void MemeAddNonParametersMethod(FolderVMBase folderVMBase)
        {
            base.MemeAddNonParametersMethod(folderVMBase);
            MemeAddNonParametersMethodAsync(folderVMBase.Id);
        }

        public virtual async void MemeAddNonParametersMethodAsync(Guid? parentFolderId)
        {
            try
            {
                IsMemesLoadedFlag = false;
                string path = dialogService.FileBrowserDialog();
                if (string.IsNullOrEmpty(path))
                    return;

                MemeDTO notSavedMemeDTO = new MemeDTO
                {
                    ParentFolderId = parentFolderId,
                    Title = Path.GetFileNameWithoutExtension(path),
                    ImagePath = path
                };

                if (notSavedMemeDTO != null)
                    await model.AddMemeAsync(notSavedMemeDTO);
            }
            catch (Exception ex)
            {
                IsMemesLoadedFlag = true;
                BusyCheck();
                OnException(ex);
            }
        }

        protected override void MemeAddMethod(FolderVMBase folderVMBase)
        {
            base.MemeAddMethod(folderVMBase);
            MemeAddMethodAsync(folderVMBase.Id);
        }

        public virtual async void MemeAddMethodAsync(Guid? parentFolderId)
        {
            try
            {
                IsMemesLoadedFlag = false;
                MemeDTO notSavedMemeDTO = await dialogService.MemeDtoOpenAddDialog(parentFolderId);
                if (notSavedMemeDTO != null)
                    await model.AddMemeAsync(notSavedMemeDTO);
                else
                {
                    IsMemesLoadedFlag = true;
                    BusyCheck();
                }
            }
            catch (Exception ex)
            {
                IsMemesLoadedFlag = true;
                BusyCheck();
                OnException(ex);
            }
        }


        protected override void MemeChangeMethod(MemeVMBase memeVMBase)
        {
            base.MemeChangeMethod(memeVMBase);
            MemeChangeMethodAsync(memeVMBase.CopyDTO());
        }

        public virtual async void MemeChangeMethodAsync(MemeDTO memeDTO)
        {
            try
            {
                IsMemesLoadedFlag = false;
                MemeDTO notSavedEditedMemeDTO = await dialogService.MemeDtoOpenEditDialog(memeDTO);
                if (notSavedEditedMemeDTO != null)
                    await model.ChangeMemeAsync(notSavedEditedMemeDTO);
                else
                {
                    IsMemesLoadedFlag = true;
                    BusyCheck();
                }
            }
            catch (Exception ex)
            {
                IsMemesLoadedFlag = true;
                BusyCheck();
                OnException(ex);
            }
        }

        protected override void MemeDeleteMethod(MemeVMBase memeVMBase)
        {
            base.MemeDeleteMethod(memeVMBase);
            MemeDeleteMethodAsync(memeVMBase.CopyDTO());
        }

        public virtual async void MemeDeleteMethodAsync(MemeDTO memeDTO)
        {
            try
            {
                IsMemesLoadedFlag = false;
                await model.DeleteMemeAsync(memeDTO);
            }
            catch (Exception ex)
            {
                IsMemesLoadedFlag = true;
                BusyCheck();
                OnException(ex);
            }
        }

        protected override void MemeDeleteTagMethod(object data)
        {
            base.MemeDeleteTagMethod(data);
            MemeDeleteTagMethodAsync(data);
        }

        public virtual async void MemeDeleteTagMethodAsync(object data)
        {
            try
            {
                object[] coll = (object[])data;
                MemeVMBase memeVMBase = coll[0] as MemeVMBase;
                MemeTagVMBase memeTagVMBase = coll[1] as MemeTagVMBase;

                IsMemesLoadedFlag = false;
                await model.DeleteMemeTagFromMemeAsync(memeVMBase.Id, memeTagVMBase.Id);
            }   
            catch(Exception ex)
            {
                BusyCheck();
                OnException(ex);
            }
        }

        protected override void MemeOpenInExplorerMethod(MemeVMBase memeVMBase)
        {
            try
            {
                base.MemeOpenInExplorerMethod(memeVMBase);

                using (Process p = new Process())
                {
                    p.StartInfo = new ProcessStartInfo("explorer", ExplorerHelper.GetImageFolderPath(memeVMBase.ImagePath)) /*{ UseShellExecute = true }*/;
                    p.Start();
                }
            }
            catch (Exception ex)
            {
                BusyCheck();
                OnException(ex);
            }
        }

        protected override void MemeOpenMethod(MemeVMBase memeVMBase)
        {
            try
            {
                base.MemeOpenMethod(memeVMBase);

                using (Process p = new Process())
                {
                    p.StartInfo = new ProcessStartInfo(memeVMBase.ImagePath) { UseShellExecute = true };
                    p.Start();
                }
            }
            catch (Exception ex)
            {
                BusyCheck();
                OnException(ex);
            }
        }

        protected override void MemeCopyMethod(MemeVMBase memeVMBase)
        {
            try
            {
                base.MemeCopyMethod(memeVMBase);
                Clipboard.SetImage(new BitmapImage(new Uri(memeVMBase.ImagePath)));
            }
            catch (Exception ex)
            {
                BusyCheck();
                OnException(ex);
            }
        }
    }
}
