using AutoMapper;
using MemeFolderN.Common.DTOClasses;
using MemeFolderN.Common.Extentions;
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
    public class FolderDataService : IFolderDataService
    {
        protected readonly MemeFolderNDbContextFactory _contextFactory;
        protected readonly IMapper _mapper;

        public virtual async Task<FolderDTO> GetById(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                Folder entity = await context.Folders
                    .Include(f => f.Memes)
                    .Include(f => f.Folders)
                    .FirstOrDefaultAsync(e => e.Id == guid);

                FolderDTO dto = _mapper.Map<FolderDTO>(entity);

                return dto;
            }
        }

        public virtual async Task<List<FolderDTO>> GetAllFolders()
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                List<Folder> entities = await context.Folders
                    .Include(f => f.Memes)
                    .ToListAsync();

                List<FolderDTO> dtos = entities.Select(f => _mapper.Map<FolderDTO>(f)).ToList();

                return dtos;
            }
        }

        [Obsolete]
        public virtual async Task<List<FolderDTO>> GetRootFolders()
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                List<Folder> folders = await context.Folders
                    .Include(m => m.Folders)
                    .Where(e => e.ParentFolder == null)
                    .ToListAsync();

                List<FolderDTO> dtos = folders.Select(f => _mapper.Map<FolderDTO>(f)).ToList();

                return dtos;
            }
        }

        public virtual async Task<List<FolderDTO>> GetFoldersByFolderID(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                List<Folder> folders = await context.Folders
                    .Include(m => m.Folders)
                    .Where(e => e.ParentFolder.Id == guid)
                    .ToListAsync();

                List<FolderDTO> dtos = folders.Select(f => _mapper.Map<FolderDTO>(f)).ToList();

                return dtos;
            }
        }

        public virtual async Task<FolderDTO> Add(FolderDTO folderDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                Folder folder = _mapper.Map<Folder>(folderDTO);

                EntityEntry<Folder> createdResult = await context.Folders.AddAsync(folder);
                await context.SaveChangesAsync();

                FolderDTO dto = _mapper.Map<FolderDTO>(createdResult.Entity);

                return dto;
            }
        }

        public virtual async Task<FolderDTO> Update(Guid guid, FolderDTO folderDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                Folder folder = _mapper.Map<Folder>(folderDTO);

                Folder dbFolder = await context.Folders.FirstOrDefaultAsync(e => e.Id == guid);

                _mapper.Map<Folder, Folder>(folder, dbFolder);

                await context.SaveChangesAsync();

                FolderDTO dto = _mapper.Map<FolderDTO>(folder);

                return dto;
            }
        }

        public virtual async Task<List<FolderDTO>> Delete(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                await context.Folders.LoadAsync();
                await context.Memes.LoadAsync();
                Folder folder = await context.Folders
                    .Include(f => f.Folders)
                    .Include(f => f.Memes)
                    .FirstOrDefaultAsync(x => x.Id == guid);

                if (folder != null)
                {
                    await context.BulkDeleteAsync(new List<Folder> { folder }, opt => opt.IncludeGraph = true);
                    await context.SaveChangesAsync();

                    List<FolderDTO> removedFolders = folder.Folders.SelectRecursive(f => f.Folders).Select(f => _mapper.Map<FolderDTO>(f)).ToList();
                    removedFolders.Add(_mapper.Map<FolderDTO>(folder));

                    return removedFolders;
                }
                else
                    throw new ArgumentNullException($"Не существует папки с guid({guid})");
            }
        }


        #region Конструкторы

        public FolderDataService()
        {
            _contextFactory = new MemeFolderNDbContextFactory();
            _mapper = new Mapper(new MapperConfiguration(opt =>
            {
                opt.AddProfile(new MapperProfileDAL());
            }));
        }

        public FolderDataService(MemeFolderNDbContextFactory contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        #endregion
    }
}
