using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemeFolderN.Core.Models
{
    [Table("MemeTagNodes")]
    public class MemeTagNode : DomainObject
    {
        [Required]
        public MemeTag MemeTag { get; set; }
        [Required]
        public Meme Meme { get; set; }

        public override string ToString() => MemeTag.Title;
    }
}
