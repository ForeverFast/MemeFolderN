using MemeFolderN.MFViewModelsBase;
using MemeFolderN.MFViewModelsBase.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MFViewModel : MFViewModelBase
    {
        protected override void NavigationByFolderMethod(IFolder folder)
        {
            base.NavigationByFolderMethod(folder);
            try 
            {
                FolderVM folderVM = (FolderVM)this.RootFolders.FirstOrDefault(rf => rf.Id == folder.Id);
                _navigationService.Navigate<FolderPage>
            }
            catch(Exception ex)
            {
                OnException(ex);
            }
        }
    }
}
