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

        public Task<List<MemeDTO>> GetMemesByFolderIdAsync(Guid id) => Task.Factory.StartNew(() => GetMemesByFolderId(id));
        protected abstract List<MemeDTO> GetMemesByFolderId(Guid id);

        public Task<List<MemeDTO>> GetMemesByTitleAsync(string title) => Task.Factory.StartNew(() => GetMemesByTitle(title));
        protected abstract List<MemeDTO> GetMemesByTitle(string title);

        public Task AddMemeAsync(MemeDTO meme) => Task.Factory.StartNew(() => AddMeme(meme));
        protected abstract void AddMeme(MemeDTO meme);

        public Task AddRangeMemesAsync(List<MemeDTO> memes) => Task.Factory.StartNew(() => AddRangeMemes(memes));
        protected abstract void AddRangeMemes(List<MemeDTO> memes);

        public Task ChangeMemeAsync(MemeDTO meme) => Task.Factory.StartNew(() => ChangeMeme(meme));
        protected abstract void ChangeMeme(MemeDTO meme);

        public Task DeleteMemeAsync(MemeDTO meme) => Task.Factory.StartNew(() => DeleteMeme(meme));
        protected abstract void DeleteMeme(MemeDTO meme);

        public Task DeleteRangeMemesAsync(List<MemeDTO> memes) => Task.Factory.StartNew(() => DeleteRangeMemes(memes));
        protected abstract void DeleteRangeMemes(List<MemeDTO> memes);
    }
}
