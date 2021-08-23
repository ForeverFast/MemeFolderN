using MemeFolderN.Core.DTOClasses;
using MemeFolderN.EntityFramework.Services;
using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFModelBase.Extentions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.MFModelBase
{
    public abstract partial class MFModelBase : IFolderModel    
    {
        protected readonly IFolderDataService folderDataService;

        public event ChangedFoldersHandler ChangedFoldersEvent;

        protected void OnRemoveFoldersEvent(List<FolderDTO> foldersDTO) => ChangedFoldersEvent?.Invoke(this, ActionType.Remove, foldersDTO);

        protected void OnAddFoldersEvent(List<FolderDTO> foldersDTO) => ChangedFoldersEvent?.Invoke(this, ActionType.Add, foldersDTO);

        protected void OnChangedFoldersEvent(List<FolderDTO> foldersDTO) => ChangedFoldersEvent?.Invoke(this, ActionType.Changed, foldersDTO);

        public Task<List<FolderDTO>> GetFoldersByFolderAsync(FolderDTO foldersDTO) => Task.Factory.StartNew(() => GetFoldersByFolder(foldersDTO));
        protected abstract List<FolderDTO> GetFoldersByFolder(FolderDTO foldersDTO);

        public Task<List<FolderDTO>> GetRootFoldersAsync() => Task.Factory.StartNew(() => GetRootFolders());
        protected abstract List<FolderDTO> GetRootFolders();

        public Task AddFolderAsync(FolderDTO folderDTO) => Task.Factory.StartNew(() => AddFolder(folderDTO));
        protected abstract void AddFolder(FolderDTO folderDTO);

        public Task ChangeFolderAsync(FolderDTO folderDTO) => Task.Factory.StartNew(() => ChangeFolder(folderDTO));
        protected abstract void ChangeFolder(FolderDTO folderDTO);

        public Task DeleteFolderAsync(FolderDTO folderDTO) => Task.Factory.StartNew(() => DeleteFolder(folderDTO));
        protected abstract void DeleteFolder(FolderDTO folderDTO);
    }
}
