using MemeFolderN.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.EntityFramework.Services
{
    public interface IMemeDataService : IDataService<Meme>
    {
        Task<IEnumerable<Meme>> FindMemesByTitle(string title);

        Task<IEnumerable<Meme>> CreateRange(params Meme[] memes);

        Task<bool> DeleteRange(params Meme[] memes);

        Task<bool> DeleteAllMemes();
    }
}
