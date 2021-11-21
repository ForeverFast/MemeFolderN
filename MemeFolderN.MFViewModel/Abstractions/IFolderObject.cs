using System;

namespace MemeFolderN.MFViewModels.Common.Abstractions
{
    public interface IFolderObject : IDomainObject
    {
        uint Position { get; set; }
        string Title { get; set; }
        string Description { get; set; }

        Guid? ParentFolderId { get; set; }
    }
}
