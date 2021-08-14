using MemeFolderN.Core.DTOClasses;
using MemeFolderN.EntityFramework.Services;
using MemeFolderN.MFModels.Abstractions;
using MemeFolderN.MFModels.Extentions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFModels
{
    public abstract class FolderModelBase : IFolderModel
    {
        private readonly IFolderDataService folderDataService;

        public bool IsDisposable { get; protected set; } = true;
        public bool IsLoaded { get; protected set; } = false;


        public event ChangedFoldersHandler ChangedFoldersEvent;

        protected void OnRemoveFoldersEvent(List<FolderDTO> folders) => ChangedFoldersEvent?.Invoke(this, ActionType.Remove, folders);
        
        protected void OnAddFoldersEvent(List<FolderDTO> folders) => ChangedFoldersEvent?.Invoke(this, ActionType.Add, folders);
     
        protected void OnChangedFoldersEvent(List<FolderDTO> folders) => ChangedFoldersEvent?.Invoke(this, ActionType.Changed, folders);

        public Task<List<FolderDTO>> GetFoldersAsync(FolderDTO folderDTO) => Task.Factory.StartNew(() => GetFolders(folderDTO));
        protected abstract List<FolderDTO> GetFolders(FolderDTO folderDTO);

        public Task DeleteFolderAsync(FolderDTO folderDTO) => Task.Factory.StartNew(() => DeleteFolder(folderDTO));
        protected abstract void DeleteFolder(FolderDTO folderDTO);

        public Task AddFolderAsync(FolderDTO folderDTO) => Task.Factory.StartNew(() => AddFolder(folderDTO));
        protected abstract void AddFolder(FolderDTO folderDTO);

        public Task ChangeFolderAsync(FolderDTO folderDTO) => Task.Factory.StartNew(() => ChangeFolder(folderDTO));
        protected abstract void ChangeFolder(FolderDTO folderDTO);

        public void Dispose()
        {
            Dispose(true);
        }
        ~FolderModelBase()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing) => IsLoaded = !(IsDisposable = true);

        protected FolderModelBase(IFolderDataService folderDataService)
        {
            this.folderDataService = folderDataService;
        }
    }
}
