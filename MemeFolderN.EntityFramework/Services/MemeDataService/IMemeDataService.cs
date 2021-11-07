using MemeFolderN.Core.DTOClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.EntityFramework.Services
{
    public interface IMemeDataService : IDataService<MemeDTO>
    {
        Task<List<MemeDTO>> GetMemesByFolderId(Guid guid);

        Task<List<MemeDTO>> GetMemesByTitle(string title);
        Task<List<MemeDTO>> GetAllMemes();

        Task<List<MemeDTO>> AddRangeMemes(List<MemeDTO> memes);

        /// <summary>
        /// Удаление сущности по Guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task<bool> Delete(Guid guid);

        Task<List<MemeDTO>> DeleteRangeMemes(List<MemeDTO> memes);

        Task<bool> DeleteAllMemes();
    }
}
