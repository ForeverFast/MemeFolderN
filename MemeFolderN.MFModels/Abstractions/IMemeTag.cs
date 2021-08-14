using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModels.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFModels.Abstractions
{
    public delegate void ChangedMemeTagsHandler(object sender, ActionType action, List<MemeTagDTO> folders);

    public interface IMemeTag : IAbstractModel
    {
        Task<List<MemeTagDTO>> GetMemesAsync();

        Task DeleteMemeTagAsync(MemeTagDTO dormitory);

        Task AddMemeTagAsync(MemeTagDTO dormitory);

        Task ChangeMemeTagAsync(MemeTagDTO dormitory);

        event ChangedMemeTagsHandler ChangedMemesEvent;
    }
}
