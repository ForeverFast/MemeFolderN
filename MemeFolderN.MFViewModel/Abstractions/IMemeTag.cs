using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModelsBase.Abstractions
{
    public interface IMemeTag : IDomainObject
    {
        public string Title { get; set; }
    }
}
