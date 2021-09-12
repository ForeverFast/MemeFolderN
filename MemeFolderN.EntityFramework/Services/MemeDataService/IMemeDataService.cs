using MemeFolderN.Core.DTOClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.EntityFramework.Services
{
    public interface IMemeDataService : IDataService<MemeDTO>
    {
        Task<IEnumerable<MemeDTO>> GetMemesByFolderId(Guid guid);

        Task<IEnumerable<MemeDTO>> GetMemesByTitle(string title);
        Task<IEnumerable<MemeDTO>> GetAllMemes();

        Task<IEnumerable<MemeDTO>> AddRangeMemes(List<MemeDTO> memes);

        Task<bool> DeleteRangeMemes(List<MemeDTO> memes);

        Task<bool> DeleteAllMemes();
    }
}
