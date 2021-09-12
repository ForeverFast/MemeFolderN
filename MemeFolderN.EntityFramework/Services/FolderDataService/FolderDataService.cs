using MemeFolderN.Core.Converters;
using MemeFolderN.Core.DTOClasses;
using MemeFolderN.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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

        public virtual async Task<IEnumerable<FolderDTO>> GetRootFolders()
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                IEnumerable<Folder> folders = await Task.FromResult(context.Folders
                    .Include(m => m.Folders)
                    .Where(e => e.ParentFolder == null).ToList());
                return folders.Select(f => f.ConvertFolder());
            }
        }

        public virtual async Task<IEnumerable<FolderDTO>> GetFoldersByFolderID(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                IEnumerable<Folder> folders = await Task.FromResult(context.Folders
                 .Include(m => m.Folders)
                 .Where(e => e.ParentFolder.Id == guid).ToList());

                return folders.Select(f => f.ConvertFolder());
            }
        }

        public virtual async Task<IEnumerable<FolderDTO>> GetAllFolders()
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                IEnumerable<Folder> entities = await Task.FromResult(context.Folders
                    .Include(f => f.Memes)
                    .ToList());
                return entities.Where(f => f.ParentFolderId == null).Select(f => f.ConvertFolder());
            }
        }

        public virtual async Task<FolderDTO> Add(FolderDTO folderDTO)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                Folder folder = folderDTO.ConvertFolderDTO();
                folder.ParentFolder = null;

                //if (folder.ParentFolderId == null && folder.ParentFolder != null)
                //{
                //    Folder parentFolder = await context.Folders
                //        .FirstOrDefaultAsync(x => x.Id == folder.ParentFolder.Id);
                //    if (parentFolder != null)
                //        folder.ParentFolder = parentFolder;
                //}
                
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

        public virtual async Task<bool> Delete(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {

                Folder folder = await context.Folders
                    .Include(f => f.Folders)
                    .FirstOrDefaultAsync(x => x.Id == guid);
                if (folder != null)
                {
                    RemoveAllData(folder, context);
                    context.Folders.Remove(folder);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                    throw new ArgumentNullException($"Не существует папки с guid({guid})");

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
