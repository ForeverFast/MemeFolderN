using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFViewModels.Default.MethodCommands;
using MemeFolderN.MFViewModels.Default.Services;
using MemeFolderN.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MemeFolderN.MFViewModels.Default.Extentions
{
    public class VmDIContainer
    {
        public readonly Dispatcher dispatcher;
        public readonly IMFModel model;
        public readonly INavigationService navigationService;
        public readonly IDialogService dialogService;
        public readonly IFolderMethodCommandsClass folderMethodCommandsClass;
        public readonly IMemeMethodCommandsClass memeMethodCommandsClass;
        public readonly IMemeTagMethodCommandsClass memeTagMethodCommandsClass;
        public readonly INavCommandsClass navCommandsClass;


        public VmDIContainer(INavigationService navigationService,
            IDialogService dialogService,
            IFolderMethodCommandsClass folderMethodCommandsClass,
            IMemeMethodCommandsClass memeMethodCommandsClass,
            IMemeTagMethodCommandsClass memeTagMethodCommandsClass,
            INavCommandsClass navCommandsClass,
            IMFModel model,
            Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            this.model = model;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.folderMethodCommandsClass = folderMethodCommandsClass;
            this.memeMethodCommandsClass = memeMethodCommandsClass;
            this.memeTagMethodCommandsClass = memeTagMethodCommandsClass;
            this.navCommandsClass = navCommandsClass;
        }
    }
}
