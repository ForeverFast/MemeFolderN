using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using System;
using System.Collections.ObjectModel;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class MemeVMBase : BaseViewModel, IMeme
    {
        public ObservableCollection<IMemeTag> MemeTags { get; } = new ObservableCollection<IMemeTag>();

        public Guid Id { get => _id; set => SetProperty(ref _id, value); }

        public DateTime AddingDate { get => _addingDate; set => SetProperty(ref _addingDate, value); }
        public string ImagePath { get => _imagePath; set => SetProperty(ref _imagePath, value); }
        public string MiniImagePath { get => _miniImagePath; set => SetProperty(ref _miniImagePath, value); }

        public uint Position { get => _position; set => SetProperty(ref _position, value); }

        public string Title { get => _title; set => SetProperty(ref _title, value); }

        public string Description { get => _description; set => SetProperty(ref _description, value); }

        public Guid? ParentFolderId { get => _parentFolderId; set => SetProperty(ref _parentFolderId, value); }

        #region Поля для хранения значений свойств
        private Guid _id;
        private DateTime _addingDate;
        private string _imagePath;
        private string _miniImagePath;
        private uint _position;
        private string _title;
        private string _description;
        private Guid? _parentFolderId;
        #endregion
    }
}
