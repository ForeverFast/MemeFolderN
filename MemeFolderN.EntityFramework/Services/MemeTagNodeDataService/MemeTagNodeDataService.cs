using MemeFolderN.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NLog;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace MemeFolderN.EntityFramework.Services
{
    public class MemeTagNodeDataService : IMemeTagNodeDataService
    {
        protected readonly MemeFolderNDbContextFactory _contextFactory;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public virtual async Task<bool> Delete(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
                {
                    MemeTagNode entity = await context.MemeTagNodes.FirstOrDefaultAsync(e => e.Id == guid);
                    context.MemeTagNodes.Remove(entity);

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

        public virtual async Task<MemeTagNode> GetById(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
                {
                    MemeTagNode entity = await context.MemeTagNodes
                        .Include(mtn => mtn.Meme)
                        .Include(mtn => mtn.MemeTag)
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

        public virtual async Task<MemeTagNode> Create(MemeTagNode memeTagNode)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
                {
                    Meme meme = await context.Memes.FirstOrDefaultAsync(m => m.Id == memeTagNode.Meme.Id);
                    if (meme != null)
                        memeTagNode.Meme = meme;
                    else
                        throw new ArgumentNullException("Meme can not be null");

                    MemeTag memeTag = await context.MemeTags.FirstOrDefaultAsync(mt => mt.Id == memeTagNode.MemeTag.Id);
                    if (memeTag != null)
                        memeTagNode.MemeTag = memeTag;
                    else
                        throw new ArgumentNullException("MemeTag can not be null");

                    EntityEntry<MemeTagNode> createdResult = await context.MemeTagNodes.AddAsync(memeTagNode);
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

        public virtual async Task<MemeTagNode> Update(Guid guid, MemeTagNode memeTagNode)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
                {
                    var original = await context.MemeTags.FirstOrDefaultAsync(e => e.Id == guid);

                    foreach (PropertyInfo propertyInfo in original.GetType().GetProperties())
                    {
                        if (propertyInfo.GetValue(memeTagNode, null) == null)
                            propertyInfo.SetValue(memeTagNode, propertyInfo.GetValue(original, null), null);
                    }
                    context.Entry(original).CurrentValues.SetValues(memeTagNode);
                    await context.SaveChangesAsync();

                    return memeTagNode;

                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Ошибка обновления");
                    return null;
                }
            }
        }

        #region Конструкторы

        public MemeTagNodeDataService()
        {
            _contextFactory = new MemeFolderNDbContextFactory();
        }

        public MemeTagNodeDataService(MemeFolderNDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #endregion
    }
}
