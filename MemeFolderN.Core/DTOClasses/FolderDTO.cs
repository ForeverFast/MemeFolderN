using System;
using System.Collections.Generic;

namespace MemeFolderN.Core.DTOClasses
{
    public class FolderDTO : FolderObjectDTO
    {
        public string FolderPath { get; }

        public DateTime CreatingDate { get; }

        public List<FolderDTO> Folders { get; }

        public List<MemeDTO> Memes { get; }

        public FolderDTO() : base()
        {

        }

        public FolderDTO(Guid id) : base(id)
        {

        }

        public FolderDTO(Guid id, string title, string description, Guid? parentFolderId) : base(id,title,description, parentFolderId)
        {

        }

        public FolderDTO(Guid id, uint position, string title, string description, Guid? parentId, FolderDTO parentFolder, 
            string folderPath, DateTime creatingDate, List<FolderDTO> folders, List<MemeDTO> memes) : base(id, position, title, description, parentId,parentFolder)
        {
            FolderPath = folderPath;
            CreatingDate = creatingDate;
            Folders = folders;
            Memes = memes;
        }
    }
}
