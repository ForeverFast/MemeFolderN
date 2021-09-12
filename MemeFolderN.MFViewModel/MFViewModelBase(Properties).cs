using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using System.Collections.ObjectModel;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class MFViewModelBase : BaseWindowViewModel, IMFViewModel
    {
        protected readonly IMFModel model;

        public ObservableCollection<IFolder> Folders { get; } = new ObservableCollection<IFolder>();
        public ObservableCollection<IMeme> Memes { get; } = new ObservableCollection<IMeme>();
        public ObservableCollection<IMemeTag> MemeTags { get; } = new ObservableCollection<IMemeTag>();

        public IFolder SelectedFolder { get => _selectedFolder; set => SetProperty(ref _selectedFolder, value); }
        public IMeme SelectedMeme { get => _selectedMeme; set => SetProperty(ref _selectedMeme, value); }
        public IMemeTag SelectedMemeTag { get => _selectedMemeTag; set => SetProperty(ref _selectedMemeTag, value); }


        #region Поля для хранения значений свойств
        private IFolder _selectedFolder;
        private IMeme _selectedMeme;
        private IMemeTag _selectedMemeTag;
        #endregion
    }
}
