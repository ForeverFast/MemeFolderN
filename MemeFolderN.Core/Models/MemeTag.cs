using System.ComponentModel.DataAnnotations.Schema;

namespace MemeFolderN.Common.Models
{
    [Table("MemeTags")]
    public class MemeTag : DomainObject
    {
        public string Title { get; set; }

        public override string ToString() => this.Title;
    }
}
