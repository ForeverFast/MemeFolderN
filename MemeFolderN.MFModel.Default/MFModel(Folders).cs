using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Extentions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MemeFolderN.MFModelBase.Wpf
{
    public partial class MFModelWpf : MFModelBase
    {
        protected override async Task<List<FolderDTO>> GetAllFolders()
        {
            List<FolderDTO> foldersDTO = await folderDataService.GetAllFolders();
            return foldersDTO;
        }

        protected override async Task<List<FolderDTO>> GetFoldersByFolderId(Guid id)
        {
            List<FolderDTO> foldersDTO = await folderDataService.GetFoldersByFolderID(id);
            return foldersDTO;
        }

        protected override async Task AddFolder(FolderDTO folderDTO)
        {
            Guid? parentGuid = folderDTO.ParentFolderId;
            string parentFolderPath = parentGuid != null ? await GetParentFolderPath(parentGuid) : userSettingsService.RootFolderPath;

            FolderDTO proccesedFolderDTO = InitNewFolder(folderDTO, parentFolderPath) with
            {
                ParentFolderId = parentGuid
            };

            FolderDTO createdFolder = await folderDataService.Add(proccesedFolderDTO);
            if (createdFolder != null)
                OnAddFoldersEvent(new List<FolderDTO>() { createdFolder });
            else
                throw new MFModelException($"Экзмпляр {proccesedFolderDTO.Title} не удалось сохранить.", MFModelExceptionEnum.NotSaved);
        }

        protected override async Task ChangeFolder(FolderDTO folderDTO)
        {
            FolderDTO updatedFolder = await folderDataService.Update(folderDTO.Id, folderDTO);
            if (updatedFolder != null)
                OnChangedFoldersEvent(new List<FolderDTO>() { updatedFolder });
            else
                throw new MFModelException($"Экзмпляр {folderDTO.Title} не удалось обновить.", MFModelExceptionEnum.NotUpdated);
        }

        protected override async Task DeleteFolder(FolderDTO folderDTO)
        {
            List<FolderDTO> result = await folderDataService.Delete(folderDTO.Id);
            if (result != null)
                OnRemoveFoldersEvent(result);
            else
                throw new MFModelException($"Экзмпляр {folderDTO.Title} не удалось удалить.", MFModelExceptionEnum.NotDeleted);
        }

        #region Вспомогательные методы

        protected FolderDTO InitNewFolder(FolderDTO folderDTO, string parentFolderPath)
        {
            string newFolderPath = string.Empty;
            if (string.IsNullOrEmpty(folderDTO.Title))
            {
                newFolderPath = GetFolderAnotherName(parentFolderPath, "Новая папка");
            }
            else
            {
                newFolderPath = @$"{parentFolderPath}\{folderDTO.Title}";
                if (Directory.Exists(newFolderPath))
                {
                    newFolderPath = GetFolderAnotherName(parentFolderPath, folderDTO.Title);
                }    
            }
            Directory.CreateDirectory(newFolderPath);

            folderDTO = folderDTO with
            {
                Title = Path.GetFileName(newFolderPath),
                FolderPath = newFolderPath
            };

            return folderDTO;
        }

        protected string GetFolderAnotherName(string rootPath, string title)
        {
            string newFolderPath = string.Empty;
            int num = 1;
            while (true)
            {
                string tempTitle = $"{title} ({num++})";

                newFolderPath = @$"{rootPath}\{tempTitle}";
                if (!Directory.Exists(newFolderPath))
                {
                    return newFolderPath;
                }
            }
        }

        #endregion
    }
}
