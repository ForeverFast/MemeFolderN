using MemeFolderN.Common.DTOClasses;
using MemeFolderN.Data.Services;
using MemeFolderN.MFModel.Common.Abstractions;
using MemeFolderN.MFModel.Common.Extentions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.MFModel.Common
{
    public abstract partial class MFModelBase : IMemeTagModel
    {
        protected readonly IMemeTagDataService memeTagDataService;
        protected readonly IMemeTagNodeDataService memeTagNodeDataService;

        public event ChangedMemeTagsHandler ChangedMemeTagsEvent;

        protected void OnRemoveMemeTagsEvent(List<MemeTagDTO> memeTags) => ChangedMemeTagsEvent?.Invoke(this, ActionType.Remove, memeTags);

        protected void OnAddMemeTagsEvent(List<MemeTagDTO> memeTags) => ChangedMemeTagsEvent?.Invoke(this, ActionType.Add, memeTags);

        protected void OnChangedMemeTagsEvent(List<MemeTagDTO> memeTags) => ChangedMemeTagsEvent?.Invoke(this, ActionType.Changed, memeTags);

        public Task<List<MemeTagDTO>> GetAllMemeTagsAsync() => Task.Run(() => GetAllMemeTags());
        protected abstract Task<List<MemeTagDTO>> GetAllMemeTags();

        public Task<List<Guid>> GetAllMemeIdByMemeTagIdAsync(Guid id) => Task.Run(() => GetAllMemeIdByMemeTagId(id));
        protected abstract Task<List<Guid>> GetAllMemeIdByMemeTagId(Guid id);

        public Task<List<MemeTagDTO>> GetMemeTagsByMemeIdAsync(Guid id) => Task.Run(() => GetMemeTagsByMemeId(id));
        protected abstract Task<List<MemeTagDTO>> GetMemeTagsByMemeId(Guid id);

        public Task AddMemeTagAsync(MemeTagDTO memeTag) => Task.Run(() => AddMemeTag(memeTag));
        protected abstract Task AddMemeTag(MemeTagDTO memeTag);

        public Task ChangeMemeTagAsync(MemeTagDTO memeTag) => Task.Run(() => ChangeMemeTag(memeTag));
        protected abstract Task ChangeMemeTag(MemeTagDTO memeTag);

        public Task DeleteMemeTagAsync(MemeTagDTO memeTag) => Task.Run(() => DeleteMemeTag(memeTag));
        protected abstract Task DeleteMemeTag(MemeTagDTO memeTag);
    }
}
