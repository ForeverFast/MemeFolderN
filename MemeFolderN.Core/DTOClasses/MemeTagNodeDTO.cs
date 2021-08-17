namespace MemeFolderN.Core.DTOClasses
{
    public class MemeTagNodeDTO : DomainObjectDTO
    {
        public MemeTagDTO MemeTag { get; set; }
        public MemeDTO Meme { get; set; }

        public override string ToString() => MemeTag.Title;
    }
}
