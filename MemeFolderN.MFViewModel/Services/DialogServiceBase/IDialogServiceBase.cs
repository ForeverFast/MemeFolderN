using MemeFolderN.Common.DTOClasses;
using System;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Common.Services
{
#nullable enable
    public delegate Task<object?> ShowDialogDelegete(object dialogFolderVM, object dialogId);
#nullable disable

    public interface IDialogServiceBase
    {
        void ShowMessage(string text);
        void ShowFolder(string path);
        string FileBrowserDialog(string Extension = "*.jpg;*.png");
        string FolderBrowserDialog();

        Task<FolderDTO> FolderDtoOpenAddDialog(Guid? parentFolderId);
        Task<FolderDTO> FolderDtoOpenEditDialog(FolderDTO folderDTO);
        Task<MemeDTO> MemeDtoOpenAddDialog(Guid? parentFolderId);
        Task<MemeDTO> MemeDtoOpenEditDialog(MemeDTO memeDTO);
        Task<MemeTagDTO> MemeTagDtoOpenAddDialog();
        Task<MemeTagDTO> MemeTagDtoOpenEditDialog(MemeTagDTO memeDTO);
    }
}
