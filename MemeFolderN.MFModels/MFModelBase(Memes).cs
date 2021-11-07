using MemeFolderN.Core.DTOClasses;
using MemeFolderN.EntityFramework.Services;
using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFModelBase.Extentions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.MFModelBase
{
    public abstract partial class MFModelBase : IMemeModel
    {
        protected readonly IMemeDataService memeDataService;

        public event ChangedMemesHandler ChangedMemesEvent;

        protected void OnRemoveMemesEvent(List<MemeDTO> memes) => ChangedMemesEvent?.Invoke(this, ActionType.Remove, memes);

        protected void OnAddMemesEvent(List<MemeDTO> memes) => ChangedMemesEvent?.Invoke(this, ActionType.Add, memes);

        protected void OnChangedMemesEvent(List<MemeDTO> memes) => ChangedMemesEvent?.Invoke(this, ActionType.Changed, memes);

        public Task<List<MemeDTO>> GetAllMemesAsync() => Task.Run(() => GetAllMemes());
        protected abstract Task<List<MemeDTO>> GetAllMemes();

        public Task<List<MemeDTO>> GetMemesByFolderIdAsync(Guid id) => Task.Run(() => GetMemesByFolderId(id));
        protected abstract Task<List<MemeDTO>> GetMemesByFolderId(Guid id);

        public Task<List<MemeDTO>> GetMemesByTitleAsync(string title) => Task.Run(() => GetMemesByTitle(title));
        protected abstract Task<List<MemeDTO>> GetMemesByTitle(string title);

        public Task AddMemeAsync(MemeDTO meme) => Task.Run(() => AddMeme(meme));
        protected abstract Task AddMeme(MemeDTO meme);

        public Task AddRangeMemesAsync(List<MemeDTO> memes) => Task.Run(() => AddRangeMemes(memes));
        protected abstract Task AddRangeMemes(List<MemeDTO> memes);

        public Task ChangeMemeAsync(MemeDTO meme) => Task.Run(() => ChangeMeme(meme));
        protected abstract Task ChangeMeme(MemeDTO meme);

        public Task DeleteMemeAsync(MemeDTO meme) => Task.Run(() => DeleteMeme(meme));
        protected abstract Task DeleteMeme(MemeDTO meme);

        public Task DeleteRangeMemesAsync(List<MemeDTO> memes) => Task.Run(() => DeleteRangeMemes(memes));
        protected abstract Task DeleteRangeMemes(List<MemeDTO> memes);

        public Task DeleteMemeTagFromMemeAsync(Guid memeGuid, Guid tagGuid) => Task.Run(() => DeleteMemeTagFromMeme(memeGuid, tagGuid));
        protected abstract Task DeleteMemeTagFromMeme(Guid memeGuid, Guid tagGuid);
    }
}
