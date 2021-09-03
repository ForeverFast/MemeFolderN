using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using System.Collections.ObjectModel;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class MFViewModelBase : BaseWindowViewModel, IMFViewModel
    {
        protected readonly IMFModel model;

        public ObservableCollection<IFolder> RootFolders { get; } = new ObservableCollection<IFolder>();

        public ObservableCollection<IMemeTag> MemeTags { get; } = new ObservableCollection<IMemeTag>();

        public IFolder SelectedFolder { get => _selectedFolder; set => SetProperty(ref _selectedFolder, value); }
        public IMemeTag SelectedMemeTag { get => _selectedMemeTag; set => SetProperty(ref _selectedMemeTag, value); }


        #region Поля для хранения значений свойств
        private IFolder _selectedFolder;
        private IMemeTag _selectedMemeTag;
        #endregion
    }
}
