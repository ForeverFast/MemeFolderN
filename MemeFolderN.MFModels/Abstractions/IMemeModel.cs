using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModels.Extentions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.MFModel.Abstractions
{
    public delegate void ChangedMemesHandler(object sender, ActionType action, List<MemeDTO> memesDTO);

    public interface IMemeModel
    {
        Task<List<MemeDTO>> GetMemesByFolderAsync(FolderDTO folder);

        Task DeleteMemeAsync(MemeDTO meme);

        Task AddMemeAsync(MemeDTO meme);

        Task ChangeMemeAsync(MemeDTO meme);

        event ChangedMemesHandler ChangedMemesEvent;
    }
}
