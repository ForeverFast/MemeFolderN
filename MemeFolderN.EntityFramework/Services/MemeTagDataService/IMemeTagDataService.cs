using MemeFolderN.Common.DTOClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.Data.Services
{
    public interface IMemeTagDataService : IDataService<MemeTagDTO>
    {
        Task<List<MemeTagDTO>> GetTags();
        Task<List<MemeTagDTO>> GetTagsByMemeId(Guid id);

        /// <summary>
        /// Удаление сущности по Guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task<bool> Delete(Guid guid);
    }
}
