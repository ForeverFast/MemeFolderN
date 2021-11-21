using MemeFolderN.Common.DTOClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.Data.Services
{
    public interface IExtentionalDataService
    {
        Task<List<FolderDTO>> BulkInsertAndUpdateFolder(FolderDTO parentFolder);
    }
}
