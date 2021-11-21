using MemeFolderN.Common.DTOClasses;
using MemeFolderN.Data.Services;
using MemeFolderN.MFModel.Common.Abstractions;
using MemeFolderN.MFModel.Common.Extentions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.MFModel.Common
{
    public abstract partial class MFModelBase : IFolderModel
    {
        protected readonly IFolderDataService folderDataService;

        public event ChangedFoldersHandler ChangedFoldersEvent;

        protected void OnRemoveFoldersEvent(List<FolderDTO> foldersDTO) => ChangedFoldersEvent?.Invoke(this, ActionType.Remove, foldersDTO);

        protected void OnAddFoldersEvent(List<FolderDTO> foldersDTO) => ChangedFoldersEvent?.Invoke(this, ActionType.Add, foldersDTO);

        protected void OnChangedFoldersEvent(List<FolderDTO> foldersDTO) => ChangedFoldersEvent?.Invoke(this, ActionType.Changed, foldersDTO);

        public Task<List<FolderDTO>> GetAllFoldersAsync() => Task.Run(() => GetAllFolders());
        protected abstract Task<List<FolderDTO>> GetAllFolders();

        public Task<List<FolderDTO>> GetFoldersByFolderIdAsync(Guid id) => Task.Run(() => GetFoldersByFolderId(id));
        protected abstract Task<List<FolderDTO>> GetFoldersByFolderId(Guid id);

        public Task AddFolderAsync(FolderDTO folderDTO) => Task.Run(() => AddFolder(folderDTO));
        protected abstract Task AddFolder(FolderDTO folderDTO);

        public Task ChangeFolderAsync(FolderDTO folderDTO) => Task.Run(() => ChangeFolder(folderDTO));
        protected abstract Task ChangeFolder(FolderDTO folderDTO);

        public Task DeleteFolderAsync(FolderDTO folderDTO) => Task.Run(() => DeleteFolder(folderDTO));
        protected abstract Task DeleteFolder(FolderDTO folderDTO);
    }
}
