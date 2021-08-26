using MemeFolderN.Core.DTOClasses;
using MemeFolderN.Core.Models;
using System.Linq;

namespace MemeFolderN.Core.Converters
{
    public static class DtoToEntityConverter
    {
        private static Folder ConvertFolderDTOBase(this FolderDTO folderDTO)
        {
            Folder folder = new Folder()
            {
                Id = folderDTO.Id,
                Title = folderDTO.Title,
                FolderPath = folderDTO.FolderPath,
                CreatingDate = folderDTO.CreatingDate,
                Description = folderDTO.Description,
                Position = folderDTO.Position,
            };

            return folder;
        }

        private static Folder ConvertFolderDTO(this FolderDTO folderDTO, Folder parentFolder)
        {
            Folder folder = folderDTO.ConvertFolderDTOBase();

            folder.ParentFolderId = folderDTO.ParentFolderId;
            folder.ParentFolder = parentFolder;
            folder.Folders = folderDTO.Folders?.Select(f => f.ConvertFolderDTO(folder));
            folder.Memes = folderDTO.Memes?.Select(m => m.ConvertMemeDTO(folder));

            return folder;
        }

        public static Folder ConvertFolderDTO(this FolderDTO folderDTO)
        {
            Folder folder = folderDTO.ConvertFolderDTOBase();

            folder.ParentFolderId = folderDTO.ParentFolderId;
            folder.ParentFolder = folderDTO.ParentFolder != null ? folderDTO.ParentFolder.ConvertFolderDTOBase() : null;
            folder.Folders = folderDTO.Folders?.Select(f => f.ConvertFolderDTO(folder));
            folder.Memes = folderDTO.Memes?.Select(m => m.ConvertMemeDTO(folder));
            
            return folder;
        }

        private static Meme ConvertMemeDTOBase(this MemeDTO memeDTO)
        {
            Meme meme = new Meme()
            {
                Id = memeDTO.Id,
                Title = memeDTO.Title,
                ImagePath = memeDTO.ImagePath,
                MiniImagePath = memeDTO.MiniImagePath,
                Description = memeDTO.Description,
                Position = memeDTO.Position,
                AddingDate = memeDTO.AddingDate,
            };

            return meme;
        }

        private static Meme ConvertMemeDTO(this MemeDTO memeDTO, Folder parentFolder)
        {
            Meme meme = memeDTO.ConvertMemeDTOBase();

            meme.ParentFolderId = parentFolder.ParentFolderId;
            meme.ParentFolder = parentFolder;
            meme.Tags = memeDTO.Tags?.Select(mtn => mtn.ConvertMemeTagDTO());

            return meme;
        }

        public static Meme ConvertMemeDTO(this MemeDTO memeDTO)
        {
            Meme meme = memeDTO.ConvertMemeDTOBase();

            meme.ParentFolderId = memeDTO.ParentFolderId;
            meme.ParentFolder = memeDTO.ParentFolder != null ? memeDTO.ParentFolder.ConvertFolderDTOBase() : null;
            meme.Tags = memeDTO.Tags?.Select(mtn => mtn.ConvertMemeTagDTO());

            return meme;
        }

        public static MemeTag ConvertMemeTagDTO(this MemeTagDTO memeTagDTO)
        {
            MemeTag memeTag = new MemeTag()
            {
                Id = memeTagDTO.Id,
                Title = memeTagDTO.Title,
            };

            return memeTag;
        }
 
        public static MemeTagNode ConvertMemeTagNodeDTO(this MemeTagNodeDTO memeTagNodeDTO)
        {
            MemeTagNode memeTagNode = new MemeTagNode()
            {
                Id = memeTagNodeDTO.Id,
                Meme = memeTagNodeDTO.Meme.ConvertMemeDTO(),
                MemeTag = memeTagNodeDTO.MemeTag.ConvertMemeTagDTO()
            };

            return memeTagNode;
        }

        public static MemeTagNode ConvertMemeTagNodeDTO(this MemeTagNodeDTO memeTagNodeDTO, Meme meme)
        {
            MemeTagNode memeTagNode = new MemeTagNode()
            {
                Id = memeTagNodeDTO.Id,
                Meme = meme,
                MemeTag = memeTagNodeDTO.MemeTag.ConvertMemeTagDTO()
            };

            return memeTagNode;
        }
    }
}
