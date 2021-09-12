using MemeFolderN.MFViewModelsBase;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MFViewModel : MFViewModelBase
    {
        public bool IsFoldersLoaded { get => _isFoldersLoaded; set => SetProperty(ref _isFoldersLoaded, value); }
        public bool IsMemesLoaded { get => _isMemesLoaded; set => SetProperty(ref _isMemesLoaded, value); }
        public bool IsMemeTagsLoaded { get => _isMemeTagsLoaded; set => SetProperty(ref _isMemeTagsLoaded, value); }


        #region Поля для хранения значений свойств
        private bool _isFoldersLoaded;
        private bool _isMemesLoaded;
        private bool _isMemeTagsLoaded;
        #endregion
    }
}
