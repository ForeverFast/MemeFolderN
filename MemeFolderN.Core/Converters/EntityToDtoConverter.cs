using MemeFolderN.Core.DTOClasses;
using MemeFolderN.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MemeFolderN.Core.Converters
{
    public static class EntityToDtoConverter
    {
        private static FolderDTO ConvertFolder(this Folder folder, Guid parentFolderId)
        {
            List<FolderDTO> folderDTOs = folder.Folders?.Select(f => f.ConvertFolder(folder.Id)).ToList();
            List<MemeDTO> memeDTOs = folder.Memes?.Select(f => f.ConvertMeme(folder.Id)).ToList();

            FolderDTO folderDTO = new FolderDTO
            {
                Id = folder.Id,
                Position = folder.Position,
                Title = folder.Title,
                Description = folder.Description,
                ParentFolderId = parentFolderId,
                ParentFolder = null,
                FolderPath = folder.FolderPath,
                CreatingDate = folder.CreatingDate,
                Folders = folderDTOs,
                Memes = memeDTOs
            };

            return folderDTO;
        }

        public static FolderDTO ConvertFolder(this Folder folder)
        {
            if (folder == null)
                return null;

            List<FolderDTO> folderDTOs = folder.Folders?.Select(f => f.ConvertFolder(folder.Id)).ToList();
            List<MemeDTO> memeDTOs = folder.Memes?.Select(f => f.ConvertMeme(folder.Id)).ToList();
            FolderDTO folderDTO = new FolderDTO
            {
                Id = folder.Id,
                Position = folder.Position,
                Title = folder.Title,
                Description = folder.Description,
                ParentFolderId = folder.ParentFolderId,
                ParentFolder = null,
                FolderPath = folder.FolderPath,
                CreatingDate = folder.CreatingDate,
                Folders = folderDTOs,
                Memes = memeDTOs
            };

            return folderDTO;
        }


        private static MemeDTO ConvertMeme(this Meme meme, Guid parentFolderId)
        {
            List<MemeTagDTO> memeTagDTOs = meme.TagNodes?.Select(mtn => mtn.ConvertMemeTagNodeToMemeTagDTO()).ToList();
            MemeDTO memeDTO = new MemeDTO
            {
                Id = meme.Id,
                Position = meme.Position,
                Title = meme.Title,
                Description = meme.Description,
                ParentFolderId = parentFolderId,
                ParentFolder = null,
                ImagePath = meme.ImagePath,
                MiniImagePath = meme.MiniImagePath,
                AddingDate = meme.AddingDate,
                Tags = memeTagDTOs
            };

            return memeDTO;
        }

        public static MemeDTO ConvertMeme(this Meme meme)
        {
            if (meme == null)
                return null;

            List<MemeTagDTO> memeTagDTOs = meme.TagNodes?.Select(mtn => mtn.ConvertMemeTagNodeToMemeTagDTO()).ToList();
            MemeDTO memeDTO = new MemeDTO
            {
                Id = meme.Id,
                Position = meme.Position,
                Title = meme.Title,
                Description = meme.Description,
                ParentFolderId = meme.ParentFolderId,
                ParentFolder = null,
                ImagePath = meme.ImagePath,
                MiniImagePath = meme.MiniImagePath,
                AddingDate = meme.AddingDate,
                Tags = memeTagDTOs
            };

            return memeDTO;
        }

        public static MemeTagDTO ConvertMemeTag(this MemeTag memeTag)
        {
            if (memeTag == null)
                return null;

            MemeTagDTO memeTagDTO = new MemeTagDTO
            {
                Id = memeTag.Id,
                Title = memeTag.Title
            };

            return memeTagDTO;
        }

        public static MemeTagNodeDTO ConvertMemeTagNode(this MemeTagNode memeTagNode)
        {
            if (memeTagNode == null)
                return null;

            MemeTagNodeDTO memeTagNodeDTO = new MemeTagNodeDTO
            {
                Id = memeTagNode.Id,
                MemeTagId = memeTagNode.MemeTag.Id,
                MemeId = memeTagNode.Meme.Id 
            };

            return memeTagNodeDTO;
        }

        public static MemeTagDTO ConvertMemeTagNodeToMemeTagDTO(this MemeTagNode memeTagNode)
        {
            if (memeTagNode == null)
                return null;

            MemeTagDTO memeTagDTO = new MemeTagDTO
            {
                Id = memeTagNode.MemeTag.Id,
                Title = memeTagNode.MemeTag.Title 
            };

            return memeTagDTO;
        }
    }
}
