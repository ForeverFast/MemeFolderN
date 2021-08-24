using System;
using System.Collections.Generic;

namespace MemeFolderN.Core.DTOClasses
{
    public class FolderDTO : FolderObject
    {
        public string FolderPath { get; set; }

        public DateTime CreatingDate { get; set; }

        public IEnumerable<FolderDTO> Folders { get; set; }

        public IEnumerable<MemeDTO> Memes { get; set; }
    }
}
