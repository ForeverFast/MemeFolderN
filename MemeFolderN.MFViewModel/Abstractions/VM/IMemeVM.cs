using MemeFolderN.MFViewModelsBase.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModelsBase.Abstractions
{
    public interface IMemeVM : IMeme
    {
        
        RelayCommand MemeChangeCommand { get; }
        RelayCommand MemeDeleteCommand { get; }
        RelayCommand MemeOpenCommand { get; }
        RelayCommand MemeCopyCommand { get; }
    }
}
