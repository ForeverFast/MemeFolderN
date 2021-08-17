using System;

namespace MemeFolderN.Core.DTOClasses
{
    public abstract class DomainObjectDTO
    {
        public Guid Id { get; set; }

        public int GetHC { get => this.GetHashCode(); }
    }
}
