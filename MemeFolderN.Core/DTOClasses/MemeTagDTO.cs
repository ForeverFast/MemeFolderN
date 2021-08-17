namespace MemeFolderN.Core.DTOClasses
{
    public class MemeTagDTO : DomainObjectDTO
    {
        public string Title { get; set; }

        public override string ToString() => this.Title;
    }
}
