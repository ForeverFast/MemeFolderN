using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MemeFolderN.MFViewModelsBase.DialogViewModels
{
    public class DialogMemeVMBase : BaseDialogViewModel, IDisposable
    {
       

        #region Поля
        private Guid _id;
        private Guid _parentFolderId;
        private string _title;
        private string _description;
        private string _imagePath;
        private MemeDTO _saveDataMeme;
        #endregion

        public MemeDTO SaveDataMeme { get => _saveDataMeme; private set => SetProperty(ref _saveDataMeme, value); }

        public Guid Id { get => _id; private set => SetProperty(ref _id, value); }
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        public string ImagePath { get => _imagePath; set => SetProperty(ref _imagePath, value); }
        public Guid ParentFolderId { get => _parentFolderId; private set => SetProperty(ref _parentFolderId, value); }

        #region Конструкторы
        public DialogMemeVMBase(MemeDTO meme,
            string dialogTitle) : base(dialogTitle)
        {
            SaveDataMeme = new MemeDTO(meme.Id, meme.Title, meme.Description);

            Id = meme.Id;
            Title = meme.Title;
            Description = meme.Description;
        }
        #endregion

        protected override void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            base.PropertyNewValue(ref fieldProperty, newValue, propertyName);
            if (propertyName == nameof(Title))
                SaveDataMeme = new MemeDTO(SaveDataMeme.Id, newValue as string, SaveDataMeme.Description, ParentFolderId, SaveDataMeme.ImagePath);

            if (propertyName == nameof(Description))
                SaveDataMeme = new MemeDTO(SaveDataMeme.Id, SaveDataMeme.Title, newValue as string, ParentFolderId, SaveDataMeme.ImagePath);

            if (propertyName == nameof(ImagePath))
                SaveDataMeme = new MemeDTO(SaveDataMeme.Id, SaveDataMeme.Title, SaveDataMeme.Description, ParentFolderId, newValue as string);
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
