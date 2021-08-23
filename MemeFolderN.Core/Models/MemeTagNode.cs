using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemeFolderN.Core.Models
{
    [Table("MemeTagNodes")]
    public class MemeTagNode : DomainObject
    {
        public Guid MemeTagId { get; set; }

        [Required]
        [ForeignKey("MemeTagId")]
        public MemeTag MemeTag { get; set; }

        public Guid MemeId { get; set; }

        [Required]
        [ForeignKey("MemeId")]
        public Meme Meme { get; set; }

        public override string ToString() => MemeTag.Title;
    }
}
