using AutoMapper;
using MemeFolderN.Common.DTOClasses;
using MemeFolderN.Common.Models;
using MemeFolderN.Data.AutoMapperProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeFolderN.Data.Services
{
    public class MemeTagDataService : IMemeTagDataService
    {
        protected readonly MemeFolderNDbContextFactory _contextFactory;
        protected readonly IMapper _mapper;

        public virtual async Task<MemeTagDTO> GetById(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                MemeTag entity = await context.MemeTags
                     .AsNoTracking()
                     .FirstOrDefaultAsync(e => e.Id == guid);

                MemeTagDTO dto = _mapper.Map<MemeTagDTO>(entity);

                return dto;
            }
        }

        public virtual async Task<List<MemeTagDTO>> GetTags()
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                List<MemeTag> memeTags = await context.MemeTags
                    .AsNoTracking()
                    .ToListAsync();

                List<MemeTagDTO> dtos = memeTags.Select(mt => _mapper.Map<MemeTagDTO>(mt)).ToList();

                return dtos;
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

                List<MemeTagDTO> dtos = memeTags.Select(mt => _mapper.Map<MemeTagDTO>(mt)).ToList();

                return dtos;
            }
        }

        public virtual async Task<MemeTagDTO> Add(MemeTagDTO memeTagDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                MemeTag memeTag = _mapper.Map<MemeTag>(memeTagDTO);

                EntityEntry<MemeTag> createdResult = await context.MemeTags.AddAsync(memeTag);
                await context.SaveChangesAsync();

                MemeTagDTO dto = _mapper.Map<MemeTagDTO>(createdResult.Entity);

                return dto;
            }
        }

        public virtual async Task<MemeTagDTO> Update(Guid guid, MemeTagDTO memeTagDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                MemeTag memeTag = _mapper.Map<MemeTag>(memeTagDTO);

                MemeTag dbMemeTag = await context.MemeTags.FirstOrDefaultAsync(e => e.Id == guid);

                _mapper.Map<MemeTag, MemeTag>(memeTag, dbMemeTag);

                await context.SaveChangesAsync();

                MemeTagDTO dto = _mapper.Map<MemeTagDTO>(memeTag);

                return dto;
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
            _mapper = new Mapper(new MapperConfiguration(opt =>
            {
                opt.AddProfile(new MapperProfileDAL());
            }));
        }

        public MemeTagDataService(MemeFolderNDbContextFactory contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        #endregion
    }
}
