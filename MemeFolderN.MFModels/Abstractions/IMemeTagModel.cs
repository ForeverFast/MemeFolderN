using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModels.Extentions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.MFModel.Abstractions
{
    public delegate void ChangedMemeTagsHandler(object sender, ActionType action, List<MemeTagDTO> memeTagsDTO);

    public interface IMemeTagModel
    {
        Task<List<MemeTagDTO>> GetMemeTagsAsync();

        Task DeleteMemeTagAsync(MemeTagDTO memeTagDTO);

        Task AddMemeTagAsync(MemeTagDTO memeTagDTO);

        Task ChangeMemeTagAsync(MemeTagDTO memeTagDTO);

        event ChangedMemeTagsHandler ChangedMemeTagsEvent;
    }
}
