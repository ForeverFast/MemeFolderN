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
    public abstract partial class MemeVMBase : BaseNavigationViewModel, IMemeVM, IMeme
    {
        protected MemeVMBase(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
