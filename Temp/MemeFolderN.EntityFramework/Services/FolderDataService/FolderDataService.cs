using MemeFolderN.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NLog;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MemeFolderN.EntityFramework.Services
{
    public class FolderDataService : IFolderDataService
    {
        private readonly MemeFolderNDbContextFactory _contextFactory;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public virtual async Task<Folder> GetById(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
                {
                    Folder entity = await Task.FromResult(context.Folders
                        .Include(f => f.Memes)
                        .FirstOrDefault(e => e.Id == guid));
                    return entity;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Ошибка получения данных");
                    return null;
                }

            }
        }

        public virtual async Task<Folder> Create(Folder folder)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
                {
                    if (folder.Title == "root")
                        throw new Exception("Fig tebe, a ne root folder");

                    Folder parentFolder = await context.Folders
                        .FirstOrDefaultAsync(x => x.Id == folder.ParentFolder.Id);
                    if (parentFolder != null)
                        folder.ParentFolder = parentFolder;

                    EntityEntry<Folder> createdResult = await context.Folders.AddAsync(folder);
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

        public virtual async Task<bool> Delete(Guid guid)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
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
                catch (Exception ex)
                {
                    logger.Error(ex, "Ошибка удаления");
                    return false;
                }
            }
        }

        public virtual async Task<Folder> Update(Guid guid, Folder folder)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                try
                {
                    var original = await context.Memes.FirstOrDefaultAsync(e => e.Id == guid);

                    foreach (PropertyInfo propertyInfo in original.GetType().GetProperties())
                    {
                        if (propertyInfo.GetValue(folder, null) == null)
                            propertyInfo.SetValue(folder, propertyInfo.GetValue(original, null), null);
                    }
                    context.Entry(original).CurrentValues.SetValues(folder);
                    await context.SaveChangesAsync();

                    return folder;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Ошибка обновления");
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
