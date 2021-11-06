using MemeFolderN.Core.Converters;
using MemeFolderN.Core.DTOClasses;
using MemeFolderN.Core.Models;
using MemeFolderN.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Z.BulkOperations;

namespace MemeFolderN.EntityFramework.Services
{
    public class FolderDataService : IFolderDataService
    {
        private readonly MemeFolderNDbContextFactory _contextFactory;

        public virtual async Task<FolderDTO> GetById(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                Folder entity = await Task.FromResult(context.Folders
                    .Include(f => f.Memes)
                    .Include(f => f.Folders)
                    .FirstOrDefault(e => e.Id == guid));

                return entity.ConvertFolder();
            }
        }

        public virtual async Task<List<FolderDTO>> GetAllFolders()
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                List<Folder> entities = await context.Folders
                    .Include(f => f.Memes)
                    .ToListAsync();

                return entities.Select(f => f.ConvertFolder()).ToList();
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

                return folders.Select(f => f.ConvertFolder()).ToList();
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

                return folders.Select(f => f.ConvertFolder()).ToList();
            }
        }

        public virtual async Task<FolderDTO> Add(FolderDTO folderDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                Folder folder = folderDTO.ConvertFolderDTO();
                folder.ParentFolder = null;

                EntityEntry<Folder> createdResult = await context.Folders.AddAsync(folder);
                await context.SaveChangesAsync();

                return createdResult.Entity.ConvertFolder();
            }
        }

        public virtual async Task<FolderDTO> Update(Guid guid, FolderDTO folderDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                Folder folder = folderDTO.ConvertFolderDTO();

                var original = await context.Folders.FirstOrDefaultAsync(e => e.Id == guid);

                foreach (PropertyInfo propertyInfo in original.GetType().GetProperties())
                {
                    if (propertyInfo.GetValue(folder, null) == null)
                        propertyInfo.SetValue(folder, propertyInfo.GetValue(original, null), null);
                }
                context.Entry(original).CurrentValues.SetValues(folder);
                await context.SaveChangesAsync();

                return folder.ConvertFolder();
            }
        }

        public virtual async Task<List<FolderDTO>> Delete(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
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

                        List<FolderDTO> removedFolders = folder.Folders.SelectRecursive(x => x.Folders).Select(x => x.ConvertFolder()).ToList();
                        removedFolders.Add(folder.ConvertFolder());
                        return removedFolders;
                    }
                    else
                        throw new ArgumentNullException($"Не существует папки с guid({guid})");
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
        }


        #region Вспомогательные методы

        private void RemoveAllData(Folder folder, MemeFolderNDbContext context)
        {
            folder = context.Folders
                .Include(f => f.Folders)
                .Include(f => f.Memes)
                .FirstOrDefault(f => f.Id == folder.Id);

            foreach (var item in folder.Folders)
            {
                RemoveAllData(item, context);
            }

            foreach (var meme in folder.Memes)
            {
                var memeEntity = context.Memes.FirstOrDefault(x => x.Id == meme.Id);
                if (memeEntity != null)
                {
                    context.Memes.Remove(memeEntity);
                }
            }
            folder.Memes.ToList().Clear();

            context.SaveChanges();
            foreach (var folder1 in folder.Folders)
            {
                var folderEntity = context.Folders.FirstOrDefault(x => x.Id == folder1.Id);
                if (folderEntity != null)
                {
                    context.Folders.Remove(folderEntity);

                }
            }
            folder.Folders.ToList().Clear();
            context.SaveChanges();
        }

        #endregion



        #region Конструкторы

        public FolderDataService()
        {
            _contextFactory = new MemeFolderNDbContextFactory();
        }

        public FolderDataService(MemeFolderNDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #endregion
    }
}
