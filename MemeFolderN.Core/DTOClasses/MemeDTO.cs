using System;
using System.Collections.Generic;

namespace MemeFolderN.Core.DTOClasses
{
    public class MemeDTO : FolderObjectDTO
    {
        public DateTime AddingDate { get; }

        public string ImagePath { get; }

        public string MiniImagePath { get; }

        public List<MemeTagNodeDTO> Tags { get; }

        public MemeDTO(Guid id, string title, string imagePath) : base(id, title)
        {
            ImagePath = imagePath;
        }

        public MemeDTO(string title, string imagePath, Guid? parentFolderId) : base(title, parentFolderId)
        {
            ImagePath = imagePath;
        }

        public MemeDTO(Guid id, uint position, string title, string description, Guid? parentId, FolderDTO parentFolder,
            DateTime addingDate, string imagePath, string miniImagePath, List<MemeTagNodeDTO> tags) : base(id, position, title, description, parentId, parentFolder)
        {
            AddingDate = addingDate;
            ImagePath = imagePath;
            MiniImagePath = miniImagePath;
            Tags = tags;
        }
    }
}
