using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase.Abstractions;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class MemeVMBase : OnPropertyChangedClass, IMeme
    {
        public MemeVMBase()
        {
            
        }

        public virtual MemeDTO CopyDTO()
           => new MemeDTO
           {
               Id = this.Id,
               Position = this.Position,
               Title = this.Title,
               Description = this.Description,
               ParentFolderId = this.ParentFolderId,
               ImagePath = this.ImagePath,
               MiniImagePath = this.MiniImagePath,
               AddingDate = this.AddingDate,
           };

        public virtual void CopyFromDTO(MemeDTO dto)
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

        public virtual bool EqualValues(MemeDTO other) =>
            Id == other.Id && ParentFolderId == other.ParentFolderId;
    }
}
