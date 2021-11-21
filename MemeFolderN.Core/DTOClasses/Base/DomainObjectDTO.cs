using System;

namespace MemeFolderN.Common.DTOClasses
{
    public abstract record DomainObjectDTO
    {
        public Guid Id { get; init; }

        public int GetHC { get => this.GetHashCode(); }

    }
}
