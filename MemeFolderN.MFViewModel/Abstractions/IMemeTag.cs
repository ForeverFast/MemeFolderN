using MemeFolderN.Common.DTOClasses;
using System.Collections.ObjectModel;

namespace MemeFolderN.MFViewModels.Common.Abstractions
{
    public interface IMemeTag : IDomainObject, ICopyDTO<MemeTagDTO>
    {
        ObservableCollection<IMeme> Memes { get; }
        public string Title { get; set; }
    }
}
