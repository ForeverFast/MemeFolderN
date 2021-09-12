using MemeFolderN.MFViewModelsBase;
using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.Navigation;
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
            
                FolderVM folderVM = (FolderVM)this.Folders.FirstOrDefault(rf => rf.Id == folder.Id);

                string navKey = folderVM.Id.ToString();
                if (navigationService.CanNavigate(navKey))
                {
                    navigationService.Navigate(navKey, NavigationType.Default);
                }
                else
                {
                    Memes.Where(m => m.ParentFolderId == folder.Id)
                        .ToList()
                        .ForEach(m => folderVM.Memes.Add(m));
                    
                    navigationService.NavigateByViewTypeKey(navKey, "folderPage", folderVM, null);
                }
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
