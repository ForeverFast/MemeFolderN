using MemeFolderN.Common.DTOClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.Data.Services
{
    public interface IMemeTagNodeDataService
    {
        /// <summary>
        /// Получение сущности по Guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task<MemeTagNodeDTO> GetById(Guid guid);
        Task<MemeTagNodeDTO> GetByMemeIdAndMemeTagId(Guid memeId, Guid memeTagId);
        Task<List<Guid>> GetAllMemeIdByMemeTagId(Guid memeTagId);

        /// <summary>
        /// Создание сущности
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<MemeDTO> Add(Guid memeId, Guid memeTagId);
        Task<MemeDTO> AddRange(Guid memeGuid, List<Guid> tags);

        /// <summary>
        /// Удаление сущности по Guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task<bool> Delete(Guid guid);
        Task<MemeDTO> Delete(Guid memeGuid, Guid tagGuid);
        Task<MemeDTO> DeleteRange(Guid memeGuid, List<Guid> tags);
    }
}
