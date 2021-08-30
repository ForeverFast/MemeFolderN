using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Default
{
    public class MemeTagVM : MemeTagVMBase
    {
        public MemeTagVM(MemeTagDTO memeTagDTO) : base()
        {
            this.CopyFromDTO(memeTagDTO);
        }
    }
}
