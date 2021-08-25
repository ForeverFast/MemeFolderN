using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModelsBase
{
    public class MemeTagVMBase : OnPropertyChangedClass, IMemeTagVM
    {
        public Guid Id { get => _id; set => SetProperty(ref _id, value); }
        public string Title { get => _title; set => SetProperty(ref _title, value); }

        public MemeTagDTO CopyDTO() =>
            new MemeTagDTO(Id, Title);

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
