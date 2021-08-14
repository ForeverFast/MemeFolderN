namespace MemeFolderN.Core.DTOClasses
{
    public class MemeTagDTO : DomainObject
    {
        public string Title { get; set; }

        public override string ToString() => this.Title;
    }
}
