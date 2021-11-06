using MemeFolderN.Core.DTOClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeFolderN.EntityFramework.Services
{
    public interface IFolderDataService : IDataService<FolderDTO>
    {
        Task<List<FolderDTO>> GetRootFolders();
        Task<List<FolderDTO>> GetFoldersByFolderID(Guid guid);
        Task<List<FolderDTO>> GetAllFolders();

        /// <summary>
        /// Удаление сущности по Guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task<List<FolderDTO>> Delete(Guid guid);
    }
}
