using System;
using System.Collections.Generic;

namespace MemeFolderN.Core.DTOClasses
{
    public record MemeDTO : FolderObjectDTO
    {
        public DateTime AddingDate { get; init; }

        public string ImagePath { get; init; }

        public string MiniImagePath { get; init; }

        public List<MemeTagDTO> Tags { get; init; }

        public List<Guid> TagGuids { get; init; }
    }
}
