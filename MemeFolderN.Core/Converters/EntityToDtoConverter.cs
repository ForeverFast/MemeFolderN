using MemeFolderN.Core.DTOClasses;
using MemeFolderN.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MemeFolderN.Core.Converters
{
    public static class EntityToDtoConverter
    {
        private static FolderDTO ConvertFolder(this Folder folder, Guid parentGuid)
        {
            List<FolderDTO> folderDTOs = folder.Folders?.Select(f => f.ConvertFolder(folder.Id)).ToList();
            List<MemeDTO> memeDTOs = folder.Memes?.Select(f => f.ConvertMeme(folder.Id)).ToList();
            FolderDTO folderDTO = new FolderDTO(folder.Id, folder.Position, folder.Title, folder.Description, parentGuid, null,
                folder.FolderPath, folder.CreatingDate, folderDTOs, memeDTOs);

            return folderDTO;
        }

        public static FolderDTO ConvertFolder(this Folder folder)
        {
            if (folder == null)
                return null;

            List<FolderDTO> folderDTOs = folder.Folders?.Select(f => f.ConvertFolder(folder.Id)).ToList();
            List<MemeDTO> memeDTOs = folder.Memes?.Select(f => f.ConvertMeme(folder.Id)).ToList();
            FolderDTO folderDTO = new FolderDTO(folder.Id, folder.Position, folder.Title, folder.Description, folder.ParentFolderId, null,
               folder.FolderPath, folder.CreatingDate, folderDTOs, memeDTOs);

            return folderDTO;
        }


        private static MemeDTO ConvertMeme(this Meme meme, Guid parentGuid)
        {
            List<MemeTagNodeDTO> memeTagNodeDTOs = meme.Tags?.Select(mtn => mtn.ConvertMemeTagNode()).ToList();
            MemeDTO memeDTO = new MemeDTO(meme.Id, meme.Position, meme.Title, meme.Description, parentGuid, null,
                meme.AddingDate, meme.ImagePath, meme.MiniImagePath, memeTagNodeDTOs);

            return memeDTO;
        }

        public static MemeDTO ConvertMeme(this Meme meme)
        {
            if (meme == null)
                return null;

            List<MemeTagNodeDTO> memeTagNodeDTOs = meme.Tags?.Select(mtn => mtn.ConvertMemeTagNode()).ToList();
            MemeDTO memeDTO = new MemeDTO(meme.Id, meme.Position, meme.Title, meme.Description, meme.ParentFolderId, null,
                meme.AddingDate, meme.ImagePath, meme.MiniImagePath, memeTagNodeDTOs);

            return memeDTO;
        }

        public static MemeTagDTO ConvertMemeTag(this MemeTag memeTag)
        {
            if (memeTag == null)
                return null;

            MemeTagDTO memeTagDTO = new MemeTagDTO(memeTag.Id, memeTag.Title);

            return memeTagDTO;
        }

        public static MemeTagNodeDTO ConvertMemeTagNode(this MemeTagNode memeTagNode)
        {
            if (memeTagNode == null)
                return null;

            MemeTagNodeDTO memeTagNodeDTO = new MemeTagNodeDTO(memeTagNode.Id, memeTagNode.MemeTag.Id, memeTagNode.Meme.Id);

            return memeTagNodeDTO;
        }
    }
}
