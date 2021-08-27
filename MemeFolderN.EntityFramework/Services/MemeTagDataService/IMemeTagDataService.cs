using MemeFolderN.Core.DTOClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.EntityFramework.Services
{
    public interface IMemeTagDataService : IDataService<MemeTagDTO>
    {
        Task<IEnumerable<MemeTagDTO>> GetTags();
        Task<IEnumerable<MemeTagDTO>> GetTagsByMemeId(Guid id);
    }
}
