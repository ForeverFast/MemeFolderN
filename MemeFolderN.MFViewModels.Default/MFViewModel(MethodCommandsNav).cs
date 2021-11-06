using MemeFolderN.MFViewModelsBase;
using MemeFolderN.MFViewModelsBase.Abstractions;
using MvvmNavigation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MFViewModel : MFViewModelBase
    {
        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            object vm = e.ViewModel;
            if (vm is FolderVM folderVM)
            {
                SelectedFolder = folderVM;
            }

            if (vm is MemeTagVM memeTagVM)
            {
                SelectedMemeTag = memeTagVM;
            }

            BusyCheck();
        }

        protected override void NavigationBackMethod()
        {
            try
            {
                base.NavigationBackMethod();
                navigationManager.GoBack();
               
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }

        protected override void NavigationForwardMethod()
        {
            try
            {
                base.NavigationForwardMethod();
                navigationManager.GoForward();
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }

        protected override void NavigationToExecute(string parameter)
        {
            try
            {
                base.NavigationToExecute(parameter);
                navigationManager.Navigate(parameter, NavigationType.Default);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }

        protected override async void NavigationByFolderMethod(IFolder folder)
        {
            try
            {
                base.NavigationByFolderMethod(folder);
                await Task.Delay(1);

                FolderVM folderVM = (FolderVM)this.Folders.FirstOrDefault(rf => rf.Id == folder.Id);

                string navKey = folderVM.Id.ToString();
                if (navigationManager.CanNavigate(navKey))
                {
                    navigationManager.Navigate(navKey, NavigationType.Default);
                }
                else
                {
                    Memes.Where(m => m.ParentFolderId == folder.Id)
                        .ToList()
                        .ForEach(m => folderVM.Memes.Add(m));
                    
                    navigationManager.NavigateByViewTypeKey(navKey, "folderPage", folderVM, null);
                }
            }
            catch(Exception ex)
            {
                BusyCheck();
                OnException(ex);
            }
        }

        protected override async void NavigationByMemeTagMethod(IMemeTag memeTag)
        {
            try
            {
                base.NavigationByMemeTagMethod(memeTag);

                MemeTagVM memeTagVM = (MemeTagVM)this.MemeTags.FirstOrDefault(rf => rf.Id == memeTag.Id);

                string navKey = memeTagVM.Id.ToString();
                if (navigationManager.CanNavigate(navKey))
                {
                    navigationManager.Navigate(navKey, NavigationType.Default);
                }
                else
                {
                    List<Guid> memeIds = await model.GetAllMemeIdByMemeTagIdAsync(memeTagVM.Id);
                    Memes.Where(m => memeIds.Any(x => x == m.Id))
                        .ToList()
                        .ForEach(m => memeTagVM.Memes.Add(m));

                    //navigationService.NavigateByViewTypeKey(navKey, "folderPage", folderVM, null);
                }

                //navCommandsClass.NavigationByFolderMethod(memeTagVM);
            }
            catch (Exception ex)
            {
                BusyCheck();
                OnException(ex);
            }
        }
    }
}
