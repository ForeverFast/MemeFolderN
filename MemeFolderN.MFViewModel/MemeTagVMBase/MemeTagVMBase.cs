using MemeFolderN.Common.DTOClasses;
using MemeFolderN.MFViewModels.Common.Abstractions;
using MemeFolderN.MFViewModels.Common.BaseViewModels;
using System;
using System.Collections.ObjectModel;

namespace MemeFolderN.MFViewModels.Common
{
    public abstract class MemeTagVMBase : BaseViewModel, IMemeTag
    {
        public ObservableCollection<IMeme> Memes { get; } = new ObservableCollection<IMeme>();

        public Guid Id { get => _id; set => SetProperty(ref _id, value); }
        public string Title { get => _title; set => SetProperty(ref _title, value); }

        public MemeTagDTO CopyDTO() =>
            new MemeTagDTO { Id = this.Id, Title = this.Title };

        public void CopyFromDTO(MemeTagDTO dto)
        {
            Id = dto.Id;
            Title = dto.Title;
        }

        public bool EqualValues(MemeTagDTO other)
            => Id == other.Id;

        #region Поля для хранения значений свойств
        private Guid _id;
        private string _title;
        #endregion
    }
}
