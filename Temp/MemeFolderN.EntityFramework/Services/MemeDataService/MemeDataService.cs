using MemeFolderN.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NLog;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace MemeFolderN.EntityFramework.Services
{
    public class MemeDataService : IMemeDataService
    {
        protected readonly MemeFolderNDbContextFactory _contextFactory;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public virtual async Task<Meme> GetById(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
                {
                    Meme entity = await context.Memes
                     .Include(m => m.ParentFolder)
                     .Include(m => m.Tags)
                         .ThenInclude(mtn => mtn.MemeTag)
                     .FirstOrDefaultAsync(e => e.Id == guid);
                    return entity;

                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Ошибка получения данных");
                    return null;
                }   
            }
        }

        public virtual async Task<Meme> Create(Meme meme)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
                {
                    if (string.IsNullOrEmpty(meme.ImagePath))
                        throw new Exception("No image path");

                    if (string.IsNullOrEmpty(meme.Title))
                        meme.Title = Path.GetFileNameWithoutExtension(meme.ImagePath);

                    Folder parentFolderEntity = await context.Folders.FirstOrDefaultAsync(x => x.Id == meme.ParentFolder.Id);
                    if (parentFolderEntity != null)
                        meme.ParentFolder = parentFolderEntity;

                    EntityEntry<Meme> createdResult = await context.Memes.AddAsync(meme);
                    await context.SaveChangesAsync();

                    return createdResult.Entity;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Ошибка создания");
                    return null;
                }
            }
        }

        public virtual async Task<bool> Delete(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
                {
                    Meme entity = await context.Memes.FirstOrDefaultAsync(e => e.Id == guid);
                    context.Memes.Remove(entity);

                    await context.SaveChangesAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Ошибка удаления");
                    return false;
                }
                
            }
        }

        public virtual async Task<Meme> Update(Guid guid, Meme meme)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
                {
                    var original = await context.Memes.FirstOrDefaultAsync(e => e.Id == guid);

                    foreach (PropertyInfo propertyInfo in original.GetType().GetProperties())
                    {
                        if (propertyInfo.GetValue(meme, null) == null)
                            propertyInfo.SetValue(meme, propertyInfo.GetValue(original, null), null);
                    }
                    context.Entry(original).CurrentValues.SetValues(meme);
                    await context.SaveChangesAsync();

                    return meme;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Ошибка обновления");
                    return null;
                }
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
