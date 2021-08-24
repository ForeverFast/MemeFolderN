using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemeFolderN.Core.Models
{
    [Table("Folders")]
    public class Folder : FolderObject
    {
        public string FolderPath { get; set; }

        public DateTime CreatingDate { get; set; }

        public IEnumerable<Folder> Folders { get; set; }

        public IEnumerable<Meme> Memes { get; set; }
    }
}
