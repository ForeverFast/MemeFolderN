using MemeFolderN.Core.DTOClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.EntityFramework.Services
{
    public interface IMemeTagNodeDataService : IDataService<MemeTagNodeDTO>
    {
        Task<MemeTagNodeDTO> GetByMemeIdAndMemeTagId(Guid memeId, Guid memeTagId);
        Task<List<Guid>> GetAllMemeIdByMemeTagId(Guid memeTagId);
        Task<List<MemeTagNodeDTO>> AddRange(List<MemeTagNodeDTO> memeTagNodeDTOs);

        /// <summary>
        /// Удаление сущности по Guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task<bool> Delete(Guid guid);
    }
}
