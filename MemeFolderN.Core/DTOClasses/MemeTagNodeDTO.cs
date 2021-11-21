using System;

namespace MemeFolderN.Common.DTOClasses
{
    public record MemeTagNodeDTO : DomainObjectDTO
    {
        public Guid MemeTagId { get; init; }
        public MemeTagDTO MemeTag { get; init; }
        public Guid MemeId { get; init; }
        public MemeDTO Meme { get; init; }

        public override string ToString() => MemeTag.Title;
    }
}
