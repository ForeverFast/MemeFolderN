using MemeFolderN.Core.DTOClasses;

namespace MemeFolderN.MFViewModelsBase.Abstractions
{
    public interface IMemeTag : IDomainObject, ICopyDTO<MemeTagDTO>
    {
        public string Title { get; set; }
    }
}
