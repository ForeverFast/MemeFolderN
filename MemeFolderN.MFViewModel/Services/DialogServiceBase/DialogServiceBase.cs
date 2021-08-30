using MemeFolderN.Core.DTOClasses;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace MemeFolderN.MFViewModelsBase.Services
{
    public class DialogServiceBase : IDialogServiceBase
    {
        private readonly string rootDialog = "RootDialog";
        private ShowDialogDelegete showDialogDelegete;

        public virtual void ShowMessage(string text) => MessageBox.Show(text);

        public virtual void ShowFolder(string path) => Process.Start("explorer", path);

        public virtual string FileBrowserDialog(string Extension = "*.jpg;*.png")
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filters.Add(new CommonFileDialogFilter("Файлы", Extension));
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                return dlg.FileName;
            else
                return "";
        }

        public virtual string FolderBrowserDialog()
        {
            var dlg = new CommonOpenFileDialog();
            dlg.IsFolderPicker = true;
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                return dlg.FileName;
            else
                return "";
        }

        public virtual async Task<FolderDTO> FolderDtoOpenAddDialog(Guid? parentFolderId)
        {
            DialogFolderVMBase dialogFolderVM = new DialogFolderVMBase(new FolderDTO { ParentFolderId = parentFolderId }, "Создание папки");

            FolderDTO newFolder = (FolderDTO) await showDialogDelegete(dialogFolderVM, rootDialog);

            return newFolder;
        }

        public virtual async Task<FolderDTO> FolderDtoOpenEditDialog(FolderDTO folderDTO)
        {
            DialogFolderVMBase dialogFolderVM = new DialogFolderVMBase(folderDTO, "Редактирование папки");

            FolderDTO editedFolder = (FolderDTO)await showDialogDelegete(dialogFolderVM, rootDialog);

            return editedFolder;
        }

        public virtual async Task<MemeDTO> MemeDtoOpenAddDialog(Guid? parentFolderId)
        {
            DialogMemeVMBase dialogMemeVM = new DialogMemeVMBase(new MemeDTO { ParentFolderId = parentFolderId }, "Создание папки", this);

            MemeDTO newMeme = (MemeDTO)await showDialogDelegete(dialogMemeVM, rootDialog);

            return newMeme;
        }

        public virtual async Task<MemeDTO> MemeDtoOpenEditDialog(MemeDTO memeDTO)
        {
            DialogMemeVMBase dialogMemeVM = new DialogMemeVMBase(memeDTO, "Редактирование папки", this);

            MemeDTO editedMeme = (MemeDTO)await showDialogDelegete(dialogMemeVM, rootDialog);

            return editedMeme;
        }

        public virtual async Task<MemeTagDTO> MemeTagDtoOpenAddDialog()
        {
            DialogTagVMBase dialogMemeTagVM = new DialogTagVMBase(new MemeTagDTO { }, "Создание папки");

            MemeTagDTO newMemeTag = (MemeTagDTO)await showDialogDelegete(dialogMemeTagVM, rootDialog);

            return newMemeTag;
        }

        public virtual async Task<MemeTagDTO> MemeTagDtoOpenEditDialog(MemeTagDTO memeDTO)
        {
            DialogTagVMBase dialogMemeTagVM = new DialogTagVMBase(memeDTO, "Редактирование папки");

            MemeTagDTO editedMemeTag = (MemeTagDTO)await showDialogDelegete(dialogMemeTagVM, rootDialog);

            return editedMemeTag;
        }

        public DialogServiceBase(ShowDialogDelegete showDialogDelegete)
        {
            this.showDialogDelegete = showDialogDelegete;
        }
    }
}
