using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using MemeFolderN.Navigation;
using System;
using System.Runtime.CompilerServices;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class MemeVMBase : BaseNavigationViewModel, IMemeVM, IMeme
    {
        protected MemeVMBase(INavigationService navigationService,
            IMFModel model) : base(navigationService)
        {
            this.model = model;
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

        public event ExceptionHandler ExceptionEvent;

        /// <summary>Вспомогательный метод для передачи сообщения об ошибке</summary>
        /// <param name="exc">Параметры ошибки</param>
        /// <param name="nameMetod">Метод отправивший сообщение</param>
        public void OnException(Exception exc, [CallerMemberName] string nameMetod = null)
            => ExceptionEvent?.Invoke(this, nameMetod, exc);
    }
}
