using MemeFolderN.Core.Models;
using MemeFolderN.Core.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NLog;
using System;
using System.Reflection;
using System.Threading.Tasks;
using MemeFolderN.Core.DTOClasses;

namespace MemeFolderN.EntityFramework.Services
{
    public class MemeTagNodeDataService : IMemeTagNodeDataService
    {
        protected readonly MemeFolderNDbContextFactory _contextFactory;
       
        
        public virtual async Task<MemeTagNodeDTO> GetById(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                MemeTagNode entity = await context.MemeTagNodes
                    .Include(mtn => mtn.Meme)
                    .Include(mtn => mtn.MemeTag)
                    .FirstOrDefaultAsync(e => e.Id == guid);
                return entity.ConvertMemeTagNode();
            }
        }

        public virtual async Task<MemeTagNodeDTO> Add(MemeTagNodeDTO memeTagNodeDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                MemeTagNode memeTagNode = memeTagNodeDTO.ConvertMemeTagNodeDTO();

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

                return createdResult.Entity.ConvertMemeTagNode();
            }
        }

        public virtual async Task<MemeTagNodeDTO> Update(Guid guid, MemeTagNodeDTO memeTagNodeDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                MemeTagNode memeTagNode = memeTagNodeDTO.ConvertMemeTagNodeDTO();

                var original = await context.MemeTags.FirstOrDefaultAsync(e => e.Id == guid);

                foreach (PropertyInfo propertyInfo in original.GetType().GetProperties())
                {
                    if (propertyInfo.GetValue(memeTagNode, null) == null)
                        propertyInfo.SetValue(memeTagNode, propertyInfo.GetValue(original, null), null);
                }
                context.Entry(original).CurrentValues.SetValues(memeTagNode);
                await context.SaveChangesAsync();

                return memeTagNode.ConvertMemeTagNode();
            }
        }

        public virtual async Task<bool> Delete(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                MemeTagNode entity = await context.MemeTagNodes.FirstOrDefaultAsync(e => e.Id == guid);
                context.MemeTagNodes.Remove(entity);

                await context.SaveChangesAsync();

                return true;
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
