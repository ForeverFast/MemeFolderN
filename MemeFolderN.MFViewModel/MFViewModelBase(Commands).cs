using MemeFolderN.MFViewModelsBase.Abstractions;
using MemeFolderN.MFViewModelsBase.BaseViewModels;
using MemeFolderN.MFViewModelsBase.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModelsBase
{
    public abstract partial class MFViewModelBase : BaseWindowViewModel, IMFViewModel
    {
        public RelayCommand FolderRootsCommand => throw new NotImplementedException();


        public RelayCommand MemeTagLoadCommand => throw new NotImplementedException();

        public RelayCommand MemeTagAddCommand => throw new NotImplementedException();

        public RelayCommand MemeTagChangeCommand => throw new NotImplementedException();

        public RelayCommand MemeTagDeleteCommand => throw new NotImplementedException();
    }
}
