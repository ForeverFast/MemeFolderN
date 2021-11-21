using System;

namespace MemeFolderN.Common.DTOClasses
{
    public record MemeTagDTO : DomainObjectDTO
    {
        public string Title { get; init; }

        public override string ToString() => this.Title;
    }
}
