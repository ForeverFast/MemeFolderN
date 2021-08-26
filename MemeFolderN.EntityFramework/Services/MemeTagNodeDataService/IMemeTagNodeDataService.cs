using MemeFolderN.Core.DTOClasses;
using System;
using System.Threading.Tasks;

namespace MemeFolderN.EntityFramework.Services
{
    public interface IMemeTagNodeDataService : IDataService<MemeTagNodeDTO>
    {
        Task<MemeTagNodeDTO> GetByMemeIdAndMemeTagId(Guid memeId, Guid memeTagId);
    }
}
