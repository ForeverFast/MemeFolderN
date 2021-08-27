using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Extentions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.MFModelBase.Abstractions
{
    public delegate void ChangedMemesHandler(object sender, ActionType action, List<MemeDTO> memesDTO);

    public interface IMemeModel
    {
        Task<List<MemeDTO>> GetMemesByFolderIdAsync(Guid id);

        Task DeleteMemeAsync(MemeDTO meme);

        Task AddMemeAsync(MemeDTO meme);

        Task ChangeMemeAsync(MemeDTO meme);

        event ChangedMemesHandler ChangedMemesEvent;
    }
}
