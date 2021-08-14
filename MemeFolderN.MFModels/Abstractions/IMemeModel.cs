using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModels.Abstractions;
using MemeFolderN.MFModels.Extentions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.Models.Abstractions
{
    public delegate void ChangedMemesHandler(object sender, ActionType action, List<MemeDTO> folders);

    public interface IMemeModel : IAbstractModel
    {
        Task<List<MemeDTO>> GetMemesAsync();

        Task DeleteMemeAsync(MemeDTO meme);

        Task AddMemeAsync(MemeDTO meme);

        Task ChangeMemeAsync(MemeDTO meme);

        event ChangedMemesHandler ChangedMemesEvent;
    }
}
