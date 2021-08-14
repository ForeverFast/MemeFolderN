using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModels.Extentions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.MFModels.Abstractions
{
    public delegate void ChangedFoldersHandler(object sender, ActionType action, List<FolderDTO> folders);

    public interface IFolderModel : IAbstractModel
    {
        Task<List<FolderDTO>> GetFoldersAsync(FolderDTO folder);

        Task DeleteFolderAsync(FolderDTO folder);

        Task AddFolderAsync(FolderDTO folder);

        Task ChangeFolderAsync(FolderDTO folder);

        event ChangedFoldersHandler ChangedFoldersEvent;
    }
}
