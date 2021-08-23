using System;

namespace MemeFolderN.Core.DTOClasses
{
    public abstract class DomainObjectDTO
    {
        public Guid Id { get; }

        public int GetHC { get => this.GetHashCode(); }

        protected DomainObjectDTO()
        {

        }

        protected DomainObjectDTO(Guid id)
        {
            Id = id;
        }
    }
}
