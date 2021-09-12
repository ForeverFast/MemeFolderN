using System;
using System.Collections.Generic;

namespace MemeFolderN.Core.DTOClasses
{
    public record FolderDTO : FolderObjectDTO
    {
        public string FolderPath { get; init; }

        public DateTime CreatingDate { get; init; }

        public List<FolderDTO> Folders { get; init; }

        public List<MemeDTO> Memes { get; init; }


    }
}
