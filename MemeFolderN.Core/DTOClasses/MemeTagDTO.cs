using System;

namespace MemeFolderN.Core.DTOClasses
{
    public class MemeTagDTO : DomainObjectDTO
    {
        public string Title { get; }

        public override string ToString() => this.Title;

        public MemeTagDTO(Guid id, string title) : base(id)
        {
            Title = title;
        }
    }
}
