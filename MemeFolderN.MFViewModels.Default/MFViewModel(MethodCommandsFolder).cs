using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Linq;
using System.Collections.Generic;
using MemeFolderN.Extentions;
using MemeFolderN.MFViewModelsBase.Abstractions;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MFViewModel : MFViewModelBase
    {
        //protected override void FolderRootsMethod()
        //{
        //    base.FolderRootsMethod();
        //    FolderRootsMethodAsync();
        //}

        //protected virtual async void FolderRootsMethodAsync()
        //{
        //    try
        //    {
        //        if (IsFoldersLoaded)
        //            return;

        //        IEnumerable<FolderDTO> folders = await model.GetRootFoldersAsync();
        //        lock (RootFolders)
        //        {
        //            foreach (FolderDTO folder in folders)
        //                RootFolders.Add(new FolderVM(vmDIContainer, folder));

        //            IsBusy = !(IsLoaded = (IsFoldersLoaded = true) && IsMemeTagsLoaded);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        OnException(ex);
        //    }
        //}

        protected override void FolderLoadMethod()
        {
            base.FolderLoadMethod();
            FolderLoadMethodAsync();
        }

        protected async void FolderLoadMethodAsync()
        {
            try
            {
                if (IsFoldersLoaded)
                    return;

                IEnumerable<FolderDTO> folders = await model.GetAllFoldersAsync();
                lock (Folders)
                {
                    foreach (FolderDTO folder in folders)
                    {
                        Folders.Add(new FolderVM(folder));
                        IEnumerable<FolderDTO> innerFolders = DataExtentions.SelectRecursive(folder.Folders, innerF => innerF.Folders);
                        foreach (FolderDTO innerFolder in innerFolders)
                            Folders.Add(new FolderVM(innerFolder));
                    }

                    IsBusy = !(IsLoaded = (IsFoldersLoaded = true) && IsMemesLoaded && IsMemeTagsLoaded);
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }


        protected override void FolderAddMethod(FolderVMBase folderVMBase)
        {
            try
            {
                base.FolderAddMethod(folderVMBase);
                folderMethodCommandsClass.FolderAddMethodAsync(folderVMBase.Id);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected override void FolderAddNonParametersMethod(FolderVMBase folderVMBase)
        {
            try
            {
                base.FolderAddNonParametersMethod(folderVMBase);
                folderMethodCommandsClass.FolderAddNonParametersMethodAsync(folderVMBase.Id);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected override void FolderChangeMethod(FolderVMBase folderVMBase)
        {
            try
            {
                base.FolderChangeMethod(folderVMBase);
                folderMethodCommandsClass.FolderChangeMethodAsync(folderVMBase);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected override void FolderDeleteMethod(FolderVMBase folderVMBase)
        {
            try
            {
                base.FolderDeleteMethod(folderVMBase);
                folderMethodCommandsClass.FolderDeleteMethodAsync(folderVMBase);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        //protected override void FolderAddMethod()
        //{
        //    try
        //    {
        //        base.FolderAddMethod();
        //        folderMethodCommandsClass.FolderAddMethodAsync(null);
        //    }
        //    catch(Exception ex)
        //    {
        //        OnException(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
        
        //protected override void FolderAddNonParametersMethod()
        //{
        //    try
        //    {
        //        base.FolderAddNonParametersMethod();
        //        folderMethodCommandsClass.FolderAddNonParametersMethodAsync(null);
        //    }
        //    catch(Exception ex)
        //    {
        //        OnException(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
    }
}
