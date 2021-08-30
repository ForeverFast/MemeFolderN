using System;

namespace MemeFolderN.Core.DTOClasses
{
    public abstract record FolderObjectDTO : DomainObjectDTO
    {
        public uint Position { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }

        public Guid? ParentFolderId { get; init; }
        public FolderDTO ParentFolder { get; init; }
        public override string ToString() => this.Title;
    }
}
