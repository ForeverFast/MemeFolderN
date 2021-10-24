using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using MemeFolderN.MFViewModelsBase.Commands;
using MemeFolderN.MFViewModelsBase.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MemeFolderN.MFViewModelsBase
{
    public class DialogMemeVMBase : BaseDialogViewModel, IDisposable
    {
        private readonly IDialogServiceBase dialogService;

        public MemeDTO SaveDataMeme { get => _saveDataMeme; private set => SetProperty(ref _saveDataMeme, value); }

        public Guid Id { get => _id; private set => SetProperty(ref _id, value); }
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        public string ImagePath { get => _imagePath; set => SetProperty(ref _imagePath, value); }
        public Guid? ParentFolderId { get => _parentFolderId; private set => SetProperty(ref _parentFolderId, value); }

        public bool CanSave
        {
            get
            {
                if (string.IsNullOrEmpty(Title))
                    return false;

                if (string.IsNullOrEmpty(ImagePath))
                    return false;

                return true;
            }
        }

        #region Команды

        public RelayCommand SetImageCommand => _setImageCommand ?? (_setImageCommand =
            new RelayCommandAction(SetImageMethod));

        private void SetImageMethod() =>
            ImagePath = dialogService.FileBrowserDialog("*.jpg;*.png");
        

        #endregion

        #region Конструкторы
        public DialogMemeVMBase(MemeDTO meme,
            string dialogTitle,
            IDialogServiceBase dialogService) : base(dialogTitle)
        {
            this.dialogService = dialogService;

            SaveDataMeme = meme with
            {
                Id = this.Id = meme.Id,
                Title = this.Title = meme.Title,
                Description = this.Description = meme.Description,
                ParentFolderId = this.ParentFolderId = meme.ParentFolderId
            };
        }
        #endregion

        protected override void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            base.PropertyNewValue(ref fieldProperty, newValue, propertyName);
            if (SaveDataMeme == null)
                return;

            if (propertyName == nameof(Title))
                SaveDataMeme = SaveDataMeme with { Title = newValue as string };
          
            if (propertyName == nameof(Description))
                SaveDataMeme = SaveDataMeme with { Description = newValue as string };

            if (propertyName == nameof(ImagePath))
                SaveDataMeme = SaveDataMeme with { ImagePath = newValue as string };

            OnPropertyChanged(nameof(CanSave));
        }

        public void Dispose()
        {
            
        }

        #region Поля для хранения значений свойств 
        private Guid _id;
        private Guid? _parentFolderId;
        private string _title;
        private string _description;
        private string _imagePath;
        private MemeDTO _saveDataMeme;
        #endregion

        #region Поля для хранения значений команд
        private RelayCommand _setImageCommand;
        #endregion
    }
}
