using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFViewModels.Default.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MemeFolderN.MFViewModels.Default.MethodCommands
{
    public class MemeMethodCommandsClass : IMemeMethodCommandsClass
    {
        private readonly IDialogService dialogService;
        private readonly IMFModel model;

        public MemeMethodCommandsClass(IDialogService dialogService, IMFModel model)
        {
            this.dialogService = dialogService;
            this.model = model;
        }

        public virtual async void MemeAddNonParametersMethodAsync(Guid? parentFolderId)
        {
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

        public virtual async void MemeAddMethodAsync(Guid? parentFolderId)
        {
            MemeDTO notSavedMemeDTO = await dialogService.MemeDtoOpenAddDialog(parentFolderId);
            if (notSavedMemeDTO != null)
                await model.AddMemeAsync(notSavedMemeDTO);
        }

        public virtual async void MemeChangeMethodAsync(MemeDTO memeDTO)
        {
            MemeDTO notSavedEditedMemeDTO = await dialogService.MemeDtoOpenEditDialog(memeDTO);
            if (notSavedEditedMemeDTO != null)
                await model.ChangeMemeAsync(notSavedEditedMemeDTO);

        }

        public virtual async void MemeDeleteMethodAsync(MemeDTO memeDTO)
        {
            await model.DeleteMemeAsync(memeDTO);
        }

        public virtual void MemeOpenMethod(string imagePath)
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(imagePath) { UseShellExecute = true };
            p.Start();
            p.Dispose();
        }

        public virtual void MemeCopyMethod(string imagePath)
        {
            Clipboard.SetImage(new BitmapImage(new Uri(imagePath)));
        }

    }
}
