using MemeFolderN.Navigation;

namespace MemeFolderN.MFViewModels.Default.MethodCommands
{
    public class NavCommandsClass : INavCommandsClass
    {
        private readonly INavigationService navigationService;

        public NavCommandsClass(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public virtual void NavigationByFolderMethod(FolderVM folderVM)
        {
            
        }
    }
}
