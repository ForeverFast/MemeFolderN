using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using System;
using System.Collections.ObjectModel;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class FolderVMBase : BaseViewModel, IFolder
    {
        public ObservableCollection<IFolder> Folders { get; } = new ObservableCollection<IFolder>();
        public ObservableCollection<IMeme> Memes { get; } = new ObservableCollection<IMeme>();

        public IFolder SelectedFolder { get => _selectedFolder; set => SetProperty(ref _selectedFolder, value); }
        public IMeme SelectedMeme { get => _selectedMeme; set => SetProperty(ref _selectedMeme, value); }

        public Guid Id { get => _id; set => SetProperty(ref _id, value); }

        public string FolderPath { get => _folderPath; set => SetProperty(ref _folderPath, value); }

        public DateTime CreatingDate { get => _creatingDate; set => SetProperty(ref _creatingDate, value); }

        public uint Position { get => _position; set => SetProperty(ref _position, value); }

        public string Title { get => _title; set => SetProperty(ref _title, value); }

        public string Description { get => _description; set => SetProperty(ref _description, value); }

        public Guid? ParentFolderId { get => _parentFolderId; set => SetProperty(ref _parentFolderId, value); }

        

        #region Поля для хранения значений свойств
        private IFolder _selectedFolder;
        private IMeme _selectedMeme;

        private string _folderPath;
        private DateTime _creatingDate;
        private uint _position;
        private string _title;
        private string _description;
        private Guid? _parentFolderId;
        private Guid _id;
        #endregion
    }
}
