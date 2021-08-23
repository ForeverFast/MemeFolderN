using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using MemeFolderN.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class FolderVMBase : BasePageViewModel, IFolderVM, IFolder
    {
        protected FolderVMBase(INavigationService navigationService
            ) : base(navigationService)
        {
        }

      

       

        
    }
}
