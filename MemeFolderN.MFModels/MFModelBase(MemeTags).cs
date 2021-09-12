using MemeFolderN.Core.DTOClasses;
using MemeFolderN.EntityFramework.Services;
using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFModelBase.Extentions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.MFModelBase
{
    public abstract partial class MFModelBase : IMemeTagModel
    {
        protected readonly IMemeTagDataService memeTagDataService;
        protected readonly IMemeTagNodeDataService memeTagNodeDataService;

        public event ChangedMemeTagsHandler ChangedMemeTagsEvent;

        protected void OnRemoveMemeTagsEvent(List<MemeTagDTO> memeTags) => ChangedMemeTagsEvent?.Invoke(this, ActionType.Remove, memeTags);

        protected void OnAddMemeTagsEvent(List<MemeTagDTO> memeTags) => ChangedMemeTagsEvent?.Invoke(this, ActionType.Add, memeTags);

        protected void OnChangedMemeTagsEvent(List<MemeTagDTO> memeTags) => ChangedMemeTagsEvent?.Invoke(this, ActionType.Changed, memeTags);

        public Task<List<MemeTagDTO>> GetAllMemeTagsAsync() => Task.Factory.StartNew(() => GetAllMemeTags());
        protected abstract List<MemeTagDTO> GetAllMemeTags();

        public Task<List<MemeTagDTO>> GetMemeTagsByMemeIdAsync(Guid id) => Task.Factory.StartNew(() => GetMemeTagsByMemeId(id));
        protected abstract List<MemeTagDTO> GetMemeTagsByMemeId(Guid id);

        public Task AddMemeTagAsync(MemeTagDTO memeTag) => Task.Factory.StartNew(() => AddMemeTag(memeTag));
        protected abstract void AddMemeTag(MemeTagDTO memeTag);

        public Task ChangeMemeTagAsync(MemeTagDTO memeTag) => Task.Factory.StartNew(() => ChangeMemeTag(memeTag));
        protected abstract void ChangeMemeTag(MemeTagDTO memeTag);

        public Task DeleteMemeTagAsync(MemeTagDTO memeTag) => Task.Factory.StartNew(() => DeleteMemeTag(memeTag));
        protected abstract void DeleteMemeTag(MemeTagDTO memeTag);
    }
}
