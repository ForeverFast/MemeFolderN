using MemeFolderN.Core.Models;

namespace MemeFolderN.Core
{
    public abstract class FolderObject : DomainObject
    {
        public uint Position { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Folder ParentFolder { get; set; }
        public override string ToString() => this.Title;

        public FolderObject()
        { }
    }
}
