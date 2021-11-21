using MemeFolderN.Common.DTOClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Common.Abstractions
{
    public interface ICopyDTO<T> where T : DomainObjectDTO
    {
        T CopyDTO();
        void CopyFromDTO(T dto);
        bool EqualValues(T other);
    }
}
