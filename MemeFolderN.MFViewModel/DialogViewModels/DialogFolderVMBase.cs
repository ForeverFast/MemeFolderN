using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using System;

namespace MemeFolderN.MFViewModelsBase.DialogViewModels
{
    public class DialogFolderVMBase : BaseDialogViewModel
    {
        #region Поля
        private Guid _id;
        private Guid _parentFolderId;
        private string _title;
        private string _description;
        private FolderDTO _saveDataFolder;
        #endregion

        public FolderDTO SaveDataFolder { get => _saveDataFolder; private set => SetProperty(ref _saveDataFolder, value); }

        public Guid Id { get => _id; private set => SetProperty(ref _id, value); }
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        public Guid ParentFolderId { get => _parentFolderId; private set => SetProperty(ref _parentFolderId, value); }

        #region Конструкторы
        public DialogFolderVMBase(Guid parentFolderId,
            string dialogTitle) : base(dialogTitle)
        {
            SaveDataFolder = new FolderDTO();
            ParentFolderId = parentFolderId;
        }

        public DialogFolderVMBase(FolderDTO folder,
            string dialogTitle) : base(dialogTitle)
        {
            SaveDataFolder = new FolderDTO(folder.Id, folder.Title, folder.Description, folder.ParentFolderId);

            Id = folder.Id;
            Title = folder.Title;
            Description = folder.Description;
        }
        #endregion

        protected override void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            base.PropertyNewValue(ref fieldProperty, newValue, propertyName);
            
            if (propertyName == nameof(Title))
                SaveDataFolder = new FolderDTO(SaveDataFolder.Id, newValue as string, SaveDataFolder.Description, ParentFolderId);

            if (propertyName == nameof(Description))
                SaveDataFolder = new FolderDTO(SaveDataFolder.Id, SaveDataFolder.Title, newValue as string, ParentFolderId);
        }
    }
}
