using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemeFolderN.Core.Models
{
    public abstract class FolderObject : DomainObject
    {
        public uint Position { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid? ParentFolderId { get; set; }

        
        [ForeignKey("ParentFolderId")]
        public Folder ParentFolder { get; set; }
        public override string ToString() => this.Title;

        public FolderObject()
        { }
    }
}
