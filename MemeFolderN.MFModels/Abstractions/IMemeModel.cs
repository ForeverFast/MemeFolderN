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
        Task<List<MemeDTO>> GetAllMemesAsync();

        Task AddMemeAsync(MemeDTO meme);

        Task ChangeMemeAsync(MemeDTO meme);

        Task DeleteMemeAsync(MemeDTO meme);
        Task DeleteMemeTagFromMemeAsync(Guid memeGuid, Guid tagGuid);

      

        event ChangedMemesHandler ChangedMemesEvent;
    }
}
