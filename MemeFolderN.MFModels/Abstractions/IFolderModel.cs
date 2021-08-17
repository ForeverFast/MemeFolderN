using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModels.Extentions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.MFModel.Abstractions
{
    public delegate void ChangedFoldersHandler(object sender, ActionType action, List<FolderDTO> foldersDTO);

    public interface IFolderModel
    {
        Task<List<FolderDTO>> GetFoldersByFolderAsync(FolderDTO folderDTO);

        Task DeleteFolderAsync(FolderDTO folderDTO);

        Task AddFolderAsync(FolderDTO folderDTO);

        Task ChangeFolderAsync(FolderDTO folderDTO);

        event ChangedFoldersHandler ChangedFoldersEvent;
    }
}
