using System;

namespace MemeFolderN.Core.DTOClasses
{
    public abstract record DomainObjectDTO
    {
        public Guid Id { get; init; }

        public int GetHC { get => this.GetHashCode(); }

    }
}
