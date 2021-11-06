using MemeFolderN.Core.DTOClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.EntityFramework.Services
{
    public interface IExtentionalDataService
    {
        Task<List<FolderDTO>> BulkInsertAndUpdateFolder(FolderDTO parentFolder);
    }
}
