using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using System;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class FolderVMBase : BaseViewModel, IFolder, IDisposable
    {
        public FolderVMBase()
        {
            
        }

        public virtual FolderDTO CopyDTO()
            => new FolderDTO
            {
                Id = this.Id,
                Position = this.Position,
                Title = this.Title,
                Description = this.Description,
                ParentFolderId = this.ParentFolderId,
                FolderPath = this.FolderPath,
                CreatingDate = this.CreatingDate,
            };

        public virtual void CopyFromDTO(FolderDTO dto)
        {
            Id = dto.Id;
            Position = dto.Position;
            Title = dto.Title;
            Description = dto.Description;
            ParentFolderId = dto.ParentFolderId;
            FolderPath = dto.FolderPath;
            CreatingDate = dto.CreatingDate;
        }

        public virtual bool EqualValues(FolderDTO other) =>
            Id == other.Id && ParentFolderId == other.ParentFolderId;
    }
}

