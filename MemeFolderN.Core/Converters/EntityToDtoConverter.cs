using MemeFolderN.Core.DTOClasses;
using MemeFolderN.Core.Models;
using System.Linq;

namespace MemeFolderN.Core.Converters
{
    public static class EntityToDtoConverter
    {
        private static FolderDTO ConvertFolderBase(this Folder folder)
        {
            FolderDTO folderDTO = new FolderDTO()
            {
                Id = folder.Id,
                Title = folder.Title,
                FolderPath = folder.FolderPath,
                CreatingDate = folder.CreatingDate,
                Description = folder.Description,
                Position = folder.Position,
            };

            return folderDTO;
        }

        private static FolderDTO ConvertFolder(this Folder folder, FolderDTO parentFolderDTO)
        {
            FolderDTO folderDTO = folder.ConvertFolderBase();

            folderDTO.ParentFolder = parentFolderDTO;
            folderDTO.Folders = folder.Folders.Select(f => f.ConvertFolder(folderDTO)).ToList();
            folderDTO.Memes = folder.Memes.Select(m => m.ConvertMeme(folderDTO)).ToList();

            return folderDTO;
        }

        public static FolderDTO ConvertFolder(this Folder folder)
        {
            FolderDTO folderDTO = folder.ConvertFolderBase();

            folderDTO.ParentFolder = folder.ParentFolder != null ? folder.ParentFolder.ConvertFolderBase() : null;
            folderDTO.Folders = folder.Folders.Select(f => f.ConvertFolder(folderDTO)).ToList();
            folderDTO.Memes = folder.Memes.Select(m => m.ConvertMeme(folderDTO)).ToList();


            return folderDTO;
        }

        private static MemeDTO ConvertMemeBase(this Meme meme)
        {
            MemeDTO memeDTO = new MemeDTO()
            {
                Id = meme.Id,
                Title = meme.Title,
                ImagePath = meme.ImagePath,
                MiniImagePath = meme.MiniImagePath,
                Description = meme.Description,
                Position = meme.Position,
                AddingDate = meme.AddingDate,
            };

            return memeDTO;
        }

        private static MemeDTO ConvertMeme(this Meme meme, FolderDTO parentFolderDTO)
        {
            MemeDTO memeDTO = meme.ConvertMemeBase();

            memeDTO.ParentFolder = parentFolderDTO;
            memeDTO.Tags = meme.Tags.ToList().Select(mtn => mtn.ConvertMemeTagNode(memeDTO)).ToList();

            return memeDTO;
        }

        public static MemeDTO ConvertMeme(this Meme meme)
        {
            MemeDTO memeDTO = meme.ConvertMemeBase();

            memeDTO.ParentFolder = memeDTO.ParentFolder != null ? meme.ParentFolder.ConvertFolderBase() : null;
            memeDTO.Tags = meme.Tags.ToList().Select(mtn => mtn.ConvertMemeTagNode(memeDTO)).ToList();

            return memeDTO;
        }

        public static MemeTagDTO ConvertMemeTag(this MemeTag memeTag)
        {
            MemeTagDTO memeTagDTO = new MemeTagDTO()
            {
                Id = memeTag.Id,
                Title = memeTag.Title,          
            };

            return memeTagDTO;
        }

        public static MemeTagNodeDTO ConvertMemeTagNode(this MemeTagNode memeTagNode)
        {
            MemeTagNodeDTO memeTagNodeDTO = new MemeTagNodeDTO()
            {
                Id = memeTagNode.Id,
                Meme = memeTagNode.Meme.ConvertMeme(),
                MemeTag = memeTagNode.MemeTag.ConvertMemeTag()
            };

            return memeTagNodeDTO;
        }

        public static MemeTagNodeDTO ConvertMemeTagNode(this MemeTagNode memeTagNode, MemeDTO memeDTO)
        {
            MemeTagNodeDTO memeTagNodeDTO = new MemeTagNodeDTO()
            {
                Id = memeTagNode.Id,
                Meme = memeDTO,
                MemeTag = memeTagNode.MemeTag.ConvertMemeTag()
            };

            return memeTagNodeDTO;
        }
    }
}
