using MemeFolderN.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.EntityFramework.Services
{
    public class MemeTagDataService : IMemeTagDataService
    {
        protected readonly MemeFolderNDbContextFactory _contextFactory;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public virtual async Task<bool> Delete(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
                {
                    MemeTag entity = await context.MemeTags.FirstOrDefaultAsync(e => e.Id == guid);
                    context.MemeTags.Remove(entity);

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

        public virtual async Task<MemeTag> GetById(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
                {
                    MemeTag entity = await context.MemeTags.FirstOrDefaultAsync(e => e.Id == guid);
                    return entity;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Ошибка получения данных");
                    return null;
                }
            }
        }

        public virtual async Task<MemeTag> Create(MemeTag meme)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
                {
                    EntityEntry<MemeTag> createdResult = await context.MemeTags.AddAsync(meme);
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

        public virtual async Task<MemeTag> Update(Guid guid, MemeTag memeTag)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
                {
                    var original = await context.MemeTags.FirstOrDefaultAsync(e => e.Id == guid);

                    foreach (PropertyInfo propertyInfo in original.GetType().GetProperties())
                    {
                        if (propertyInfo.GetValue(memeTag, null) == null)
                            propertyInfo.SetValue(memeTag, propertyInfo.GetValue(original, null), null);
                    }
                    context.Entry(original).CurrentValues.SetValues(memeTag);
                    await context.SaveChangesAsync();

                    return memeTag;

                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Ошибка обновления");
                    return null;
                }
            }
        }

        #region Конструкторы

        public MemeTagDataService()
        {
            _contextFactory = new MemeFolderNDbContextFactory();
        }

        public MemeTagDataService(MemeFolderNDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #endregion
    }
}
