namespace MemeFolderN.Core.DTOClasses
{
    public abstract class FolderObjectDTO : DomainObjectDTO
    {
        public uint Position { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public FolderDTO ParentFolder { get; set; }
        public override string ToString() => this.Title;

        public FolderObjectDTO()
        { }
    }
}
