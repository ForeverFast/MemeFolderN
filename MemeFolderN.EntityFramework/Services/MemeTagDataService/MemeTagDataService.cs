using MemeFolderN.Core.DTOClasses;
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
using MemeFolderN.Core.Converters;

namespace MemeFolderN.EntityFramework.Services
{
    public class MemeTagDataService : IMemeTagDataService
    {
        protected readonly MemeFolderNDbContextFactory _contextFactory;
       
       
        public virtual async Task<MemeTagDTO> GetById(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                MemeTag entity = await context.MemeTags.FirstOrDefaultAsync(e => e.Id == guid);
                return entity.ConvertMemeTag();
            }
        }

        public virtual async Task<List<MemeTagDTO>> GetTags()
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                List<MemeTag> memeTags = await context.MemeTags.ToListAsync();
                return memeTags.Select(mt => mt.ConvertMemeTag()).ToList();
            }
        }

        public virtual async Task<List<MemeTagDTO>> GetTagsByMemeId(Guid id)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                List<MemeTag> memeTags = await context.MemeTagNodes
                    .Include(mt => mt.MemeTag)
                    .Include(mt => mt.Meme)
                    .Where(mtn => mtn.MemeId == id)
                    .Select(mtn => mtn.MemeTag)
                    .ToListAsync();

                return memeTags.Select(mt => mt.ConvertMemeTag()).ToList();
            }
        }

        public virtual async Task<MemeTagDTO> Add(MemeTagDTO memeTagDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                MemeTag memeTag = memeTagDTO.ConvertMemeTagDTO();

                EntityEntry<MemeTag> createdResult = await context.MemeTags.AddAsync(memeTag);
                await context.SaveChangesAsync();

                return createdResult.Entity.ConvertMemeTag();
            }
        }

        public virtual async Task<MemeTagDTO> Update(Guid guid, MemeTagDTO memeTagDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                MemeTag memeTag = memeTagDTO.ConvertMemeTagDTO();

                var original = await context.MemeTags.FirstOrDefaultAsync(e => e.Id == guid);

                foreach (PropertyInfo propertyInfo in original.GetType().GetProperties())
                {
                    if (propertyInfo.GetValue(memeTag, null) == null)
                        propertyInfo.SetValue(memeTag, propertyInfo.GetValue(original, null), null);
                }
                context.Entry(original).CurrentValues.SetValues(memeTag);
                await context.SaveChangesAsync();

                return memeTag.ConvertMemeTag();
            }
        }
     
        public virtual async Task<bool> Delete(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                MemeTag entity = await context.MemeTags.FirstOrDefaultAsync(e => e.Id == guid);
                context.MemeTags.Remove(entity);

                await context.SaveChangesAsync();

                return true;
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
