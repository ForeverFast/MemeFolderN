using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Extentions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.MFModelBase.Abstractions
{
    public delegate void ChangedFoldersHandler(object sender, ActionType action, List<FolderDTO> foldersDTO);

    public interface IFolderModel
    {
        Task<List<FolderDTO>> GetFoldersByFolderIdAsync(Guid id);
        Task<List<FolderDTO>> GetAllFoldersAsync();
        Task DeleteFolderAsync(FolderDTO folderDTO);

        Task AddFolderAsync(FolderDTO folderDTO);

        Task ChangeFolderAsync(FolderDTO folderDTO);

        event ChangedFoldersHandler ChangedFoldersEvent;
    }
}
