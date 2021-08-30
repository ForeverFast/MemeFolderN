using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using System;

namespace MemeFolderN.MFViewModelsBase
{
    public class DialogTagVMBase : BaseDialogViewModel
    {
        public MemeTagDTO SaveDataMemeTag { get => _saveDataMemeTag; private set => SetProperty(ref _saveDataMemeTag, value); }

        public Guid Id { get => _id; private set => SetProperty(ref _id, value); }
        public string Title { get => _title; set => SetProperty(ref _title, value); }


        public DialogTagVMBase(MemeTagDTO memeTag, string dialogTitle) : base(dialogTitle)
        {
            SaveDataMemeTag = new MemeTagDTO
            {
                Id = this.Id = memeTag.Id,
                Title = this.Title = memeTag.Title
            };
        }

        protected override void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            base.PropertyNewValue(ref fieldProperty, newValue, propertyName);
            if (propertyName == nameof(Title))
                SaveDataMemeTag = SaveDataMemeTag with { Title = newValue as string };
        }

        #region Поля для хранения значений свойств 
        private Guid _id;
        private string _title;
        private MemeTagDTO _saveDataMemeTag;
        #endregion
    }
}

