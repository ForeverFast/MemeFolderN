using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemeFolderN.Common.Models
{
    [Table("Folders")]
    public class Folder : FolderObject
    {
        public string FolderPath { get; set; }

        public DateTime CreatingDate { get; set; }

        public List<Folder> Folders { get; set; }

        public List<Meme> Memes { get; set; }

        public Folder()
        {
            Folders = new List<Folder>();
            Memes = new List<Meme>();
        }
    }
}
