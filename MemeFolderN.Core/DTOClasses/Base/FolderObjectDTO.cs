using System;

namespace MemeFolderN.Core.DTOClasses
{
    public abstract class FolderObjectDTO : DomainObjectDTO
    {
        public uint Position { get; }
        public string Title { get; }
        public string Description { get; }

        public Guid? ParentFolderId { get; }
        public FolderDTO ParentFolder { get; }
        public override string ToString() => this.Title;

        protected FolderObjectDTO(string title) : base()
        {
            Title = title;
        }

        protected FolderObjectDTO(string title, Guid? parentFolderId) : this(title)
        {
            ParentFolderId = parentFolderId;
        }

        protected FolderObjectDTO(Guid id) : base(id)
        {
            
        }

        protected FolderObjectDTO(Guid id, string title) : this(id)
        {
            Title = title;
        }

        protected FolderObjectDTO(Guid id, uint position, string title, string description, Guid? parentFolderId, FolderDTO parentFolder) : base(id)
        {
            Position = position;
            Title = title;
            Description = description;
            ParentFolderId = parentFolderId;
            ParentFolder = parentFolder;
        }
    }
}
