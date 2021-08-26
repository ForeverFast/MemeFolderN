using MemeFolderN.Core.DTOClasses;
using MemeFolderN.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MemeFolderN.Core.Converters;

namespace MemeFolderN.EntityFramework.Services
{
    public class MemeDataService : IMemeDataService
    {
        protected readonly MemeFolderNDbContextFactory _contextFactory;
        
        public virtual async Task<MemeDTO> GetById(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                Meme entity = await context.Memes
                    .Include(m => m.ParentFolder)
                    .Include(m => m.TagNodes)
                        .ThenInclude(mtn => mtn.MemeTag)
                    .FirstOrDefaultAsync(e => e.Id == guid);
                return entity.ConvertMeme();
            }
        }

        public virtual async Task<IEnumerable<MemeDTO>> GetMemesByFolderId(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                IEnumerable<Meme> memes = await Task.FromResult(context.Memes
                     .Include(m => m.ParentFolder)
                     .Include(m => m.TagNodes)
                         .ThenInclude(mtn => mtn.MemeTag)
                     .Where(e => e.Id == guid).ToList());
                return memes.Select(m => m.ConvertMeme());
            }
        }

        public virtual async Task<IEnumerable<MemeDTO>> GetMemesByTitle(string title)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                IEnumerable<Meme> memes = await Task.FromResult(context.Memes
                 .Include(m => m.ParentFolder)
                 .Include(m => m.TagNodes)
                     .ThenInclude(mtn => mtn.MemeTag)
                 .Where(e => e.Title == title).ToList());

                return memes.Select(m => m.ConvertMeme());
            }
        }


        public virtual async Task<MemeDTO> Add(MemeDTO memeDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                Meme meme = memeDTO.ConvertMemeDTO();

                if (string.IsNullOrEmpty(meme.ImagePath))
                    throw new ArgumentNullException("No image path");

                if (string.IsNullOrEmpty(meme.Title))
                    meme.Title = Path.GetFileNameWithoutExtension(meme.ImagePath);


                if (meme.ParentFolder != null)
                {
                    Folder parentFolderEntity = await context.Folders.FirstOrDefaultAsync(x => x.Id == meme.ParentFolder.Id);
                    if (parentFolderEntity == null)
                        throw new ArgumentException($"ParentFolder with Guid = '{meme.ParentFolder.Id}' does not exist");
                    meme.ParentFolder = parentFolderEntity;
                }

                EntityEntry<Meme> createdResult = await context.Memes.AddAsync(meme);
                await context.SaveChangesAsync();

                return createdResult.Entity.ConvertMeme();
            }
        }

        public virtual async Task<IEnumerable<MemeDTO>> AddRangeMemes(List<MemeDTO> memesDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                IEnumerable<Meme> memes = memesDTO.Select(mDTO => mDTO.ConvertMemeDTO());

                List<MemeDTO> SavedMemes = new List<MemeDTO>();
                List<MemeDTO> UnsavedMemes = new List<MemeDTO>();
                foreach (Meme meme in memes)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(meme.ImagePath))
                            throw new ArgumentNullException("No image path");

                        if (string.IsNullOrEmpty(meme.Title))
                            meme.Title = Path.GetFileNameWithoutExtension(meme.ImagePath);

                        Folder parentFolderEntity = null;
                        if (meme.ParentFolder != null)
                        {
                            parentFolderEntity = await context.Folders.FirstOrDefaultAsync(x => x.Id == meme.ParentFolder.Id);
                            if (parentFolderEntity == null)
                                throw new ArgumentException($"ParentFolder with Guid = '{meme.ParentFolder.Id}' does not exist");
                            meme.ParentFolder = parentFolderEntity;
                        }

                        EntityEntry<Meme> createdResult = await context.Memes.AddAsync(meme);
                        SavedMemes.Add(createdResult.Entity.ConvertMeme());
                        await context.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        UnsavedMemes.Add(meme.ConvertMeme());
                    }
                }
                return SavedMemes;
            }
        }


        public virtual async Task<MemeDTO> Update(Guid guid, MemeDTO memeDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                Meme meme = memeDTO.ConvertMemeDTO();

                var original = await context.Memes.FirstOrDefaultAsync(e => e.Id == guid);

                foreach (PropertyInfo propertyInfo in original.GetType().GetProperties())
                {
                    if (propertyInfo.GetValue(meme, null) == null)
                        propertyInfo.SetValue(meme, propertyInfo.GetValue(original, null), null);
                }
                context.Entry(original).CurrentValues.SetValues(meme);
                await context.SaveChangesAsync();

                return meme.ConvertMeme();
            }
        }

        
        public virtual async Task<bool> Delete(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                Meme entity = await context.Memes.FirstOrDefaultAsync(e => e.Id == guid);
                context.Memes.Remove(entity);

                await context.SaveChangesAsync();

                return true;
            }
        }

        public virtual async Task<bool> DeleteRangeMemes(List<MemeDTO> memesDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                IEnumerable<Meme> memes = memesDTO.Select(mDto => mDto.ConvertMemeDTO());
                context.RemoveRange(memes);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public virtual async Task<bool> DeleteAllMemes()
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                IEnumerable<Meme> memes = await Task.FromResult(context.Memes.ToList());
                context.RemoveRange(memes);
                await context.SaveChangesAsync();

                return true;
            }
        }

        #region Конструкторы

        public MemeDataService()
        {
            _contextFactory = new MemeFolderNDbContextFactory();
        }

        public MemeDataService(MemeFolderNDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #endregion
    }
}
