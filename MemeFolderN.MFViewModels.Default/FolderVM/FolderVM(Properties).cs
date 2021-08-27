using MemeFolderN.MFViewModelsBase;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class FolderVM : FolderVMBase
    {
        public bool IsFoldersLoaded { get => _isFoldersLoaded; set => SetProperty(ref _isFoldersLoaded, value); }
        public bool IsMemesLoaded { get => _isMemesLoaded; set => SetProperty(ref _isMemesLoaded, value); }

        #region Поля для хранения значений свойств
        private bool _isFoldersLoaded;
        private bool _isMemesLoaded;
        #endregion
    }
}
