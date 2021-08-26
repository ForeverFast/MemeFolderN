using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase.DialogViewModels;
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

        public virtual async Task<FolderDTO> FolderDtoOpenDialog(Guid parentFolderId)
        {
            DialogFolderVMBase dialogFolderVM = new DialogFolderVMBase(new FolderDTO(), "Создание папки");

            FolderDTO newFolder = (FolderDTO) await showDialogDelegete(dialogFolderVM, rootDialog);

            return newFolder;
        }

        public DialogServiceBase(ShowDialogDelegete showDialogDelegete)
        {
            this.showDialogDelegete = showDialogDelegete;
        }
    }
}
