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
            try
            {
                base.NavigationByFolderMethod(folder);
            
                FolderVM folderVM = (FolderVM)this.RootFolders.FirstOrDefault(rf => rf.Id == folder.Id);

                navCommandsClass.NavigationByFolderMethod(folderVM);
            }
            catch(Exception ex)
            {
                OnException(ex);
            }
        }

        protected override void NavigationByMemeTagMethod(IMemeTag memeTag)
        {
            try
            {
                base.NavigationByMemeTagMethod(memeTag);

                MemeTagVM memeTagVM = (MemeTagVM)this.MemeTags.FirstOrDefault(rf => rf.Id == memeTag.Id);

                // todo

                //navCommandsClass.NavigationByFolderMethod(memeTagVM);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }
    }
}
