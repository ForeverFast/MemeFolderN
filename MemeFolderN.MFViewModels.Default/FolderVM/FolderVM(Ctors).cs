using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFModelBase.Extentions;
using MemeFolderN.MFViewModels.Default.Services;
using MemeFolderN.MFViewModelsBase;

using MemeFolderN.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class FolderVM : FolderVMBase
    {
        /// <summary>Диспетчер UI потока</summary>
        private readonly Dispatcher dispatcher;

        private readonly IDialogService dialogService;

        public FolderVM(INavigationService navigationService,
            IDialogService dialogService,
            IMFModel model,
            Dispatcher dispatcher) : base(navigationService, model)
        {
            this.dispatcher = dispatcher;
            this.dialogService = dialogService;

            model.ChangedFoldersEvent += Model_ChangedFoldersEvent;
            model.ChangedMemesEvent += Model_ChangedMemesEvent;
        }

        public FolderVM(INavigationService navigationService,
            IDialogService dialogService,
            IMFModel model,
            Dispatcher dispatcher,
            FolderDTO folderDTO) : this(navigationService, dialogService, model, dispatcher)
        {
            this.CopyFromDTO(folderDTO);
        }



        public override void Dispose()
        {
            
        }
    }
}
