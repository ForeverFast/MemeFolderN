using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using MemeFolderN.Navigation;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class FolderVMBase : BasePageViewModel, IFolderVM, IFolder
    {
        protected FolderVMBase(INavigationService navigationService,
            IMFModel model) : base(navigationService)
        {
            this.model = model;
        }

        public FolderDTO CopyDTO()
            => new FolderDTO(Id, Position, Title, Description, ParentFolderId, null,
                FolderPath, CreatingDate, null, null);

        public void CopyFromDTO(FolderDTO dto)
        {
            Id = dto.Id;
            Position = dto.Position;
            Title = dto.Title;
            Description = dto.Description;
            ParentFolderId = dto.ParentFolderId;
            FolderPath = dto.FolderPath;
            CreatingDate = dto.CreatingDate;
        }

        public bool EqualValues(FolderDTO other) =>
            Id == other.Id && ParentFolderId == other.ParentFolderId;
    }
}

// Не удалять!
// Folders.Select(f => f.CopyDTO())?.ToList(), Memes.Select(m => m.CopyDTO())?.ToList()
