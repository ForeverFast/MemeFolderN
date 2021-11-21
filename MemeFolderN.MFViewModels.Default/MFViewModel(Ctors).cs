using GongSolutions.Wpf.DragDrop;
using MemeFolderN.MFModel.Wpf.Abstractions;
using MemeFolderN.MFViewModels.Wpf.Extentions;
using MemeFolderN.MFViewModels.Wpf.Services;
using MemeFolderN.MFViewModels.Common;
using System.Windows.Threading;

namespace MemeFolderN.MFViewModels.Wpf
{
    public partial class MFViewModel : MFViewModelBase, IDropTarget
    {
        private readonly IMFModelWpf model;

        /// <summary>Диспетчер UI потока</summary>
        private readonly Dispatcher dispatcher;
        private readonly IDialogService dialogService;

        public MFViewModel(VmDIContainer vmDIContainer) : base(vmDIContainer.navigationManager)
        {
            this.model = vmDIContainer.model;

            this.dispatcher = vmDIContainer.dispatcher;
            this.dialogService = vmDIContainer.dialogService;

            model.ChangedFoldersEvent += Model_ChangedFoldersEvent;
            model.ChangedMemesEvent += Model_ChangedMemesEvent;
            model.ChangedMemeTagsEvent += Model_ChangedMemeTagsEvent;
            navigationManager.Navigated += NavigationService_Navigated;

            OnAllPropertyChanged();
        }

        // Только для Времени Разработки
        public MFViewModel() : base(null)
        { }

        public void BusyCheck() => IsBusy = !(IsMemesLoadedFlag && IsFoldersLoadedFlag && IsMemeTagsLoadedFlag);
        public void LoadCheck() => IsLoaded = IsMemesLoadedFlag && IsFoldersLoadedFlag && IsMemeTagsLoadedFlag;

        protected override void CleanUp(bool clean)
        {
            if (!this._disposed)
            {
                if (clean)
                {
                    
                    this.Folders.Clear();
                    this.Memes.Clear();
                    this.MemeTags.Clear();

                    this.SelectedFolder = null;
                    this.SelectedMeme = null;
                    this.SelectedMemeTag = null;
                }
            }
            this._disposed = true;
        }

        ~MFViewModel()
        {
            CleanUp(false);
        }
    }
}
