using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Extentions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.MFModelBase.Abstractions
{
    public delegate void ChangedMemeTagsHandler(object sender, ActionType action, List<MemeTagDTO> memeTagsDTO);

    public interface IMemeTagModel
    {
        Task<List<MemeTagDTO>> GetAllMemeTagsAsync();

        Task<List<MemeTagDTO>> GetMemeTagsByMemeIdAsync(Guid id);

        Task DeleteMemeTagAsync(MemeTagDTO memeTagDTO);

        Task AddMemeTagAsync(MemeTagDTO memeTagDTO);

        Task ChangeMemeTagAsync(MemeTagDTO memeTagDTO);

        event ChangedMemeTagsHandler ChangedMemeTagsEvent;
    }
}
