using MemeFolderN.Common.DTOClasses;
using MemeFolderN.MFViewModels.Common.BaseViewModels;
using System;

namespace MemeFolderN.MFViewModels.Common
{
    public class DialogFolderVMBase : BaseDialogViewModel
    {
        #region Поля
        private Guid _id;
        private Guid? _parentFolderId;
        private string _title;
        private string _description;
        private FolderDTO _saveDataFolder;
        #endregion

        public FolderDTO SaveDataFolder { get => _saveDataFolder; private set => SetProperty(ref _saveDataFolder, value); }

        public Guid Id { get => _id; private set => SetProperty(ref _id, value); }
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        public Guid? ParentFolderId { get => _parentFolderId; private set => SetProperty(ref _parentFolderId, value); }

        #region Конструкторы
        public DialogFolderVMBase(Guid parentFolderId,
            string dialogTitle) : base(dialogTitle)
        {
            SaveDataFolder = new FolderDTO 
            {
               ParentFolderId = this.ParentFolderId = parentFolderId
            };
        }

        public DialogFolderVMBase(FolderDTO folder,
            string dialogTitle) : base(dialogTitle)
        {
            SaveDataFolder = folder with
            {
                Id = this.Id = folder.Id,
                Title = this.Title = folder.Title,
                Description = this.Description = folder.Description,
                ParentFolderId = this.ParentFolderId = folder.ParentFolderId
            };
        }
        #endregion

        protected override void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            base.PropertyNewValue(ref fieldProperty, newValue, propertyName);

            if (SaveDataFolder == null)
                return;

            if (propertyName == nameof(Title))
                SaveDataFolder = SaveDataFolder with { Title = newValue as string };

            if (propertyName == nameof(Description))
                SaveDataFolder = SaveDataFolder with { Description = newValue as string };
        }
    }
}
