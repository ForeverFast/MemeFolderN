using AutoMapper;
using MemeFolderN.Common.DTOClasses;
using MemeFolderN.Common.Models;
using MemeFolderN.Data.AutoMapperProfiles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeFolderN.Data.Services
{
    public class MemeTagNodeDataService : IMemeTagNodeDataService
    {
        protected readonly MemeFolderNDbContextFactory _contextFactory;
        protected readonly IMapper _mapper;

        public virtual async Task<MemeTagNodeDTO> GetById(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                MemeTagNode memeTagNode = await context.MemeTagNodes
                    .Include(mtn => mtn.Meme)
                    .Include(mtn => mtn.MemeTag)
                    .FirstOrDefaultAsync(e => e.Id == guid);

                MemeTagNodeDTO dto = _mapper.Map<MemeTagNodeDTO>(memeTagNode);

                return dto;
            }
        }

        public virtual async Task<MemeTagNodeDTO> GetByMemeIdAndMemeTagId(Guid memeId, Guid memeTagId)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                MemeTagNode memeTagNode = await context.MemeTagNodes
                    .Include(mtn => mtn.Meme)
                    .Include(mtn => mtn.MemeTag)
                    .FirstOrDefaultAsync(mtn => mtn.MemeId == memeId && mtn.MemeTagId == memeTagId);

                MemeTagNodeDTO dto = _mapper.Map<MemeTagNodeDTO>(memeTagNode);

                return dto;
            }
        }

        public virtual async Task<List<Guid>> GetAllMemeIdByMemeTagId(Guid memeTagId)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                List<Guid> result = await context.MemeTagNodes
                    .Include(mtn => mtn.Meme)
                    .Include(mtn => mtn.MemeTag)
                    .Where(x => x.MemeTagId == memeTagId)
                    .Select(x => x.MemeId)
                    .ToListAsync();

                return result;
            }
        }


        public virtual async Task<MemeDTO> Add(Guid memeGuid, Guid tagGuid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                MemeTagNode memeTagNode = new MemeTagNode();

                bool memeResult = context.Memes.Any(m => m.Id == memeGuid);
                if (memeResult)
                    memeTagNode.MemeId = memeGuid;
                else
                    throw new ArgumentNullException("Meme can not be null");

                bool memeTagResult = context.MemeTags.Any(mt => mt.Id == tagGuid);
                if (memeTagResult)
                    memeTagNode.MemeTagId = tagGuid;
                else
                    throw new ArgumentNullException("MemeTag can not be null");

                await context.MemeTagNodes.AddAsync(memeTagNode);
                await context.SaveChangesAsync();

                MemeDTO dto = _mapper.Map<MemeDTO>(await context.Memes
                   .AsNoTracking()
                   .FirstOrDefaultAsync(m => m.Id == memeGuid));

                return dto;
            }
        }

        public virtual async Task<MemeDTO> AddRange(Guid memeGuid, List<Guid> tags)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                List<MemeTagNode> mtns = tags.Select(t => new MemeTagNode
                { 
                    MemeId = memeGuid,
                    MemeTagId = t
                }).ToList();

                await context.BulkInsertAsync(mtns);
                await context.SaveChangesAsync();

                MemeDTO dto = _mapper.Map<MemeDTO>(await context.Memes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == memeGuid));

                return dto;
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

        public virtual async Task<MemeDTO> Delete(Guid memeGuid, Guid tagGuid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                MemeTagNode entity = await context.MemeTagNodes
                    .Include(mtn => mtn.Meme)
                    .FirstOrDefaultAsync(e => e.MemeId == memeGuid && e.MemeTagId == tagGuid);

                context.MemeTagNodes.Remove(entity);

                await context.SaveChangesAsync();

                MemeDTO dto = _mapper.Map<MemeDTO>(await context.Memes
                   .AsNoTracking()
                   .FirstOrDefaultAsync(m => m.Id == memeGuid));

                return dto;
            }
        }

        public virtual async Task<MemeDTO> DeleteRange(Guid memeGuid, List<Guid> tags)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                List<MemeTagNode> mtns = await context.MemeTagNodes
                    .Where(mtn => mtn.MemeId == memeGuid && tags.Any(t => t == mtn.MemeTagId))
                    .ToListAsync();

                context.BulkDelete(mtns);

                await context.SaveChangesAsync();

                MemeDTO dto = _mapper.Map<MemeDTO>(await context.Memes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == memeGuid));

                return dto;
            }
        }


        #region Конструкторы

        public MemeTagNodeDataService()
        {
            _contextFactory = new MemeFolderNDbContextFactory();
            _mapper = new Mapper(new MapperConfiguration(opt =>
            {
                opt.AddProfile(new MapperProfileDAL());
            }));
        }

        public MemeTagNodeDataService(MemeFolderNDbContextFactory contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
     
        #endregion
    }
}
