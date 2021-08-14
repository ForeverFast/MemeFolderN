namespace MemeFolderN.Core.DTOClasses
{
    public class MemeTagNodeDTO : DomainObject
    {
        public MemeTagDTO MemeTag { get; set; }
        public MemeDTO Meme { get; set; }

        public override string ToString() => MemeTag.Title;
    }
}
