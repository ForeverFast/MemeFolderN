using System;

namespace MemeFolderN.Core.DTOClasses
{
    public class MemeTagNodeDTO : DomainObjectDTO
    {
        public Guid MemeTagId { get; }
        public MemeTagDTO MemeTag { get; }
        public Guid MemeId { get; }
        public MemeDTO Meme { get; }

        public override string ToString() => MemeTag.Title;

        public MemeTagNodeDTO(Guid id, MemeTagDTO memeTag, MemeDTO meme) : base(id)
        {
            MemeTag = memeTag;
            Meme = meme;
        }

        public MemeTagNodeDTO(Guid id, Guid memeTagId, Guid memeId) : base(id)
        {
            MemeTagId = memeTagId;
            MemeId = memeId;
        }
    }
}
