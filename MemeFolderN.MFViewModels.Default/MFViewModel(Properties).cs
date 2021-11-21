using MemeFolderN.MFViewModels.Common;

namespace MemeFolderN.MFViewModels.Wpf
{
    public partial class MFViewModel : MFViewModelBase
    {
        public bool IsFoldersLoadedFlag { get => _isFoldersLoadedFlag; set => SetProperty(ref _isFoldersLoadedFlag, value); }
        public bool IsMemesLoadedFlag { get => _isMemesLoadedFlag; set => SetProperty(ref _isMemesLoadedFlag, value); }
        public bool IsMemeTagsLoadedFlag { get => _isMemeTagsLoadedFlag; set => SetProperty(ref _isMemeTagsLoadedFlag, value); }


        #region Поля для хранения значений свойств
        private bool _isFoldersLoadedFlag = false;
        private bool _isMemesLoadedFlag = false;
        private bool _isMemeTagsLoadedFlag = false;
        #endregion
    }
}
