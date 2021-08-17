using System;
using System.Collections.Generic;

namespace MemeFolderN.Core.DTOClasses
{
    public class FolderDTO : FolderObjectDTO
    {
        public string FolderPath { get; set; }

        public DateTime CreatingDate { get; set; }

        public List<FolderDTO> Folders { get; set; }

        public List<MemeDTO> Memes { get; set; }
    }
}
