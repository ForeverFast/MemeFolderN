using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemeFolderN.Core
{
    public abstract class DomainObject
    {
        [Key]
        public Guid Id { get; set; }

        [NotMapped]
        public int GetHC { get => this.GetHashCode(); }
    }
}
