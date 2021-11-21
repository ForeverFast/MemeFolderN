using MemeFolderN.Common.DTOClasses;
using MemeFolderN.MFModel.Common.Extentions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.MFModel.Common.Abstractions
{
    public delegate void ChangedMemeTagsHandler(object sender, ActionType action, List<MemeTagDTO> memeTagsDTO);

    public interface IMemeTagModel
    {
        Task<List<MemeTagDTO>> GetAllMemeTagsAsync();
        Task<List<Guid>> GetAllMemeIdByMemeTagIdAsync(Guid id);

        Task<List<MemeTagDTO>> GetMemeTagsByMemeIdAsync(Guid id);

        Task DeleteMemeTagAsync(MemeTagDTO memeTagDTO);

        Task AddMemeTagAsync(MemeTagDTO memeTagDTO);

        Task ChangeMemeTagAsync(MemeTagDTO memeTagDTO);

        event ChangedMemeTagsHandler ChangedMemeTagsEvent;
    }
}
