using MemeFolderN.Core.DTOClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.EntityFramework.Services
{
    public interface IFolderDataService : IDataService<FolderDTO>
    {
        Task<IEnumerable<FolderDTO>> GetRootFolders();
        Task<IEnumerable<FolderDTO>> GetFoldersByFolderID(Guid guid);
        Task<IEnumerable<FolderDTO>> GetAllFolders();
    }
}
