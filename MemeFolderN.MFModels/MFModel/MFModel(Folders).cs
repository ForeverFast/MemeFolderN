﻿using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModel.Extentions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MemeFolderN.MFModel.MFModel
{
    public partial class MFModel
    {
        protected override List<FolderDTO> GetFoldersByFolder(FolderDTO folderDTO)
        {
            IEnumerable<FolderDTO> foldersDTO = folderDataService.GetFoldersByFolderID(folderDTO.Id).Result;
            return foldersDTO.ToList();
        }

        protected override List<FolderDTO> GetRootFolders()
        {
            IEnumerable<FolderDTO> foldersDTO = folderDataService.GetRootFolders().Result;
            return foldersDTO.ToList();
        }

        protected override void AddFolder(FolderDTO folderDTO)
        {
            FolderDTO parentFolder = folderDTO.ParentFolder;

            string newFolderPath = string.Empty;
            if (string.IsNullOrEmpty(folderDTO.Title))
            {
                newFolderPath = GetFolderAnotherName(parentFolder.FolderPath, "Новая папка");
            }
            else
            {
                newFolderPath = @$"{parentFolder.FolderPath}\{folderDTO.Title}";
                if (Directory.Exists(newFolderPath))
                    newFolderPath = GetFolderAnotherName(parentFolder.FolderPath, folderDTO.Title);
                Directory.CreateDirectory(newFolderPath);
            }

            folderDTO.Title = Path.GetFileName(newFolderPath);
            folderDTO.FolderPath = newFolderPath;

            FolderDTO createdFolder = folderDataService.Add(folderDTO).Result;
            if (createdFolder != null)
                OnAddFoldersEvent(new List<FolderDTO>() { createdFolder });
            else
                throw new MFModelException($"Экзмпляр {folderDTO.Title} не удалось сохранить.", MFModelExceptionEnum.NotSaved);
        }

        protected override void ChangeFolder(FolderDTO folderDTO)
        {
            FolderDTO updatedFolder = folderDataService.Update(folderDTO.Id, folderDTO).Result;
            if (updatedFolder != null)
                OnChangedFoldersEvent(new List<FolderDTO>() { updatedFolder });
            else
                throw new MFModelException($"Экзмпляр {folderDTO.Title} не удалось обновить.", MFModelExceptionEnum.NotUpdated);
        }

        protected override void DeleteFolder(FolderDTO folderDTO)
        {
            if (folderDataService.Delete(folderDTO.Id).Result)
                OnRemoveFoldersEvent(new List<FolderDTO>() { folderDTO });
            else
                throw new MFModelException($"Экзмпляр {folderDTO.Title} не удалось удалить.", MFModelExceptionEnum.NotDeleted);
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
                    Directory.CreateDirectory(newFolderPath);
                    break;
                }
            }

            return newFolderPath;
        }
    }
}