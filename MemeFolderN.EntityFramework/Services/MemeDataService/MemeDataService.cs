using AutoMapper;
using MemeFolderN.Core.DTOClasses;
using MemeFolderN.Core.Models;
using MemeFolderN.EntityFramework.AutoMapperProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MemeFolderN.EntityFramework.Services
{
    public class MemeDataService : IMemeDataService
    {
        protected readonly MemeFolderNDbContextFactory _contextFactory;
        protected readonly IMapper _mapper;

        public virtual async Task<MemeDTO> GetById(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                Meme meme = await context.Memes
                    .Include(m => m.TagNodes)
                        .ThenInclude(mtn => mtn.MemeTag)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == guid);

                MemeDTO dto = _mapper.Map<MemeDTO>(meme);

                return dto;
            }
        }

        public virtual async Task<List<MemeDTO>> GetMemesByFolderId(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                List<Meme> memes = await context.Memes
                    .Include(m => m.TagNodes)
                        .ThenInclude(mtn => mtn.MemeTag)
                    .AsNoTracking()
                    .Where(m => m.ParentFolderId == guid)
                    .ToListAsync();

                List<MemeDTO> dtos = memes.Select(m => _mapper.Map<MemeDTO>(m)).ToList();

                return dtos;
            }
        }

        public virtual async Task<List<MemeDTO>> GetMemesByTitle(string title)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                List<Meme> memes = await context.Memes
                    .Include(m => m.TagNodes)
                        .ThenInclude(mtn => mtn.MemeTag)
                    .AsNoTracking()
                    .Where(e => e.Title == title)
                    .ToListAsync();

                List<MemeDTO> dtos = memes.Select(m => _mapper.Map<MemeDTO>(m)).ToList();

                return dtos;
            }
        }

        public virtual async Task<List<MemeDTO>> GetAllMemes()
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                List<Meme> memes = await context.Memes
                    .Include(m => m.TagNodes)
                        .ThenInclude(mtn => mtn.MemeTag)
                    .AsNoTracking()
                    .ToListAsync();

                List<MemeDTO> dtos = memes.Select(m => _mapper.Map<MemeDTO>(m)).ToList();

                return dtos;
            }
        }

        public virtual async Task<MemeDTO> Add(MemeDTO memeDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                Meme meme = _mapper.Map<Meme>(memeDTO);

                if (string.IsNullOrEmpty(meme.ImagePath))
                    throw new ArgumentNullException("Нет пути к изображению");

                if (string.IsNullOrEmpty(meme.Title))
                    meme.Title = Path.GetFileNameWithoutExtension(meme.ImagePath);

                if (meme.ParentFolderId == null)
                    throw new ArgumentNullException("Нет родительской папки.");

                EntityEntry<Meme> createdResult = await context.Memes.AddAsync(meme);
                await context.SaveChangesAsync();

                MemeDTO dto = _mapper.Map<MemeDTO>(createdResult.Entity);

                return dto;
            }
        }

        public virtual async Task<List<MemeDTO>> AddRangeMemes(List<MemeDTO> memesDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                await Task.Delay(1);

                //IEnumerable<Meme> memes = memesDTO.Select(mDTO => mDTO.ConvertMemeDTO());

                //List<MemeDTO> SavedMemes = new List<MemeDTO>();
                //List<MemeDTO> UnsavedMemes = new List<MemeDTO>();
                //foreach (Meme meme in memes)
                //{
                //    try
                //    {
                //        if (string.IsNullOrEmpty(meme.ImagePath))
                //            throw new ArgumentNullException("No image path");

                //        if (string.IsNullOrEmpty(meme.Title))
                //            meme.Title = Path.GetFileNameWithoutExtension(meme.ImagePath);

                //        Folder parentFolderEntity = null;
                //        if (meme.ParentFolder != null)
                //        {
                //            parentFolderEntity = await context.Folders.FirstOrDefaultAsync(x => x.Id == meme.ParentFolder.Id);
                //            if (parentFolderEntity == null)
                //                throw new ArgumentException($"ParentFolder with Guid = '{meme.ParentFolder.Id}' does not exist");
                //            meme.ParentFolder = parentFolderEntity;
                //        }

                //        EntityEntry<Meme> createdResult = await context.Memes.AddAsync(meme);
                //        SavedMemes.Add(createdResult.Entity.ConvertMeme());
                //        await context.SaveChangesAsync();
                //    }
                //    catch (Exception)
                //    {
                //        UnsavedMemes.Add(meme.ConvertMeme());
                //    }
                //}
                return null;
            }
        }


        public virtual async Task<MemeDTO> Update(Guid guid, MemeDTO memeDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                Meme meme = _mapper.Map<Meme>(memeDTO);

                var original = await context.Memes.FirstOrDefaultAsync(e => e.Id == guid);

                foreach (PropertyInfo propertyInfo in original.GetType().GetProperties())
                {
                    if (propertyInfo.GetValue(meme, null) == null)
                        propertyInfo.SetValue(meme, propertyInfo.GetValue(original, null), null);
                }
                context.Entry(original).CurrentValues.SetValues(meme);
                await context.SaveChangesAsync();
                
                return _mapper.Map<MemeDTO>(meme);
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

        public virtual async Task<List<MemeDTO>> DeleteRangeMemes(List<MemeDTO> memesDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                IEnumerable<Meme> memes = context.Memes
                    .Where(m => memesDTO.Any(x => x.Id == m.Id))
                    .Select(x => _mapper.Map<Meme>(x));

                context.BulkDelete(memes, opt => opt.IncludeGraph = true);

                await context.SaveChangesAsync();

                return memes.Select(m => _mapper.Map<MemeDTO>(m)).ToList();
            }
        }

        public virtual async Task<bool> DeleteAllMemes()
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                IEnumerable<Meme> memes = await context.Memes
                    .Include(m => m.TagNodes)
                        .ThenInclude(mtn => mtn.MemeTag)
                    .ToListAsync();

                context.BulkDelete(memes, opt => opt.IncludeGraph = true);
                await context.SaveChangesAsync();

                return true;
            }
        }



        #region Конструкторы

        public MemeDataService()
        {
            _contextFactory = new MemeFolderNDbContextFactory();
            _mapper = new Mapper(new MapperConfiguration(opt =>
            {
                opt.AddProfile(new MapperProfileDAL());
            }));
        }

        public MemeDataService(MemeFolderNDbContextFactory contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        #endregion
    }
}
