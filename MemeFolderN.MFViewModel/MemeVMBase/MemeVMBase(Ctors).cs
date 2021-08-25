using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using MemeFolderN.Navigation;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class MemeVMBase : BaseNavigationViewModel, IMemeVM, IMeme
    {
        protected MemeVMBase(INavigationService navigationService,
            IMFModel model) : base(navigationService)
        {
            this.model = model;
        }

        public MemeDTO CopyDTO()
           => new MemeDTO(Id, Position, Title, Description, ParentFolderId, null,
               AddingDate, ImagePath, MiniImagePath, null);

        public void CopyFromDTO(MemeDTO dto)
        {
            Id = dto.Id;
            Position = dto.Position;
            Title = dto.Title;
            Description = dto.Description;
            ParentFolderId = dto.ParentFolderId;
            AddingDate = dto.AddingDate;
            ImagePath = dto.ImagePath;
            MiniImagePath = dto.MiniImagePath;
        }

        public bool EqualValues(MemeDTO other) =>
            Id == other.Id && ParentFolderId == other.ParentFolderId;
    }
}
