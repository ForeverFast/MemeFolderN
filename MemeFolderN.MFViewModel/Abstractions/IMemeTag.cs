using MemeFolderN.Core.DTOClasses;
using System.Collections.ObjectModel;

namespace MemeFolderN.MFViewModelsBase.Abstractions
{
    public interface IMemeTag : IDomainObject, ICopyDTO<MemeTagDTO>
    {
        ObservableCollection<IMeme> Memes { get; }
        public string Title { get; set; }
    }
}
