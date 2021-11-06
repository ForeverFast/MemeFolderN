using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemeFolderN.Core.Models
{
    [Table("Memes")]
    public class Meme : FolderObject
    {
        public DateTime AddingDate { get; set; }

        public string ImagePath { get; set; }

        public string MiniImagePath { get; set; }

        public List<MemeTagNode> TagNodes { get; set; }

        [NotMapped]
        public List<MemeTag> Tags { get; set; }

        public Meme() : base()
        {
            AddingDate = DateTime.Now;
            TagNodes = new List<MemeTagNode>();
            Tags = new List<MemeTag>();
        }
    }
}
