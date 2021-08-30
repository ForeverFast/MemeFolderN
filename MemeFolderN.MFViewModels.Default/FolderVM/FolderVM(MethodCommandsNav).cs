using MemeFolderN.MFViewModelsBase;
using MemeFolderN.MFViewModelsBase.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class FolderVM : FolderVMBase
    {
        protected override void NavigationByFolderMethod(IFolder folder)
        {
            try
            {
                base.NavigationByFolderMethod(folder);

                FolderVM folderVM = (FolderVM)this.Folders.FirstOrDefault(rf => rf.Id == folder.Id);

                navCommandsClass.NavigationByFolderMethod(folderVM);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }
    }
}
