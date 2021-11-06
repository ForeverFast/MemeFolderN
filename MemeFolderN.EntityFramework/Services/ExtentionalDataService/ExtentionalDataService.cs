using MemeFolderN.Core.Converters;
using MemeFolderN.Core.DTOClasses;
using MemeFolderN.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.BulkOperations;

namespace MemeFolderN.EntityFramework.Services
{
    public class ExtentionalDataService : IExtentionalDataService
    {
        private readonly MemeFolderNDbContextFactory _contextFactory;

        public virtual async Task<List<FolderDTO>> BulkInsertAndUpdateFolder(FolderDTO parentFolder)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                Folder folder = await context.Folders.FirstOrDefaultAsync(x => x.Id == parentFolder.Id);

                if (folder != null)
                {
                    //List<Folder> dbFolders = folders.Select(x => x.ConvertFolderDTO()).ToList();
                    folder.Folders = parentFolder.Folders.Select(x => x.ConvertFolderDTO()).ToList();
                    folder.Memes = parentFolder.Memes.Select(x => x.ConvertMemeDTO()).ToList();
                    List<Folder> dbFolders = new() { folder };
                    
                    context.BulkInsert(dbFolders, options =>
                    {
                        options.IncludeGraphOperationBuilder = operation =>
                        {
                            if (operation is BulkOperation<Folder>)
                            {
                                var bulk = (BulkOperation<Folder>)operation;
                                bulk.InsertIfNotExists = true;
                                bulk.ColumnPrimaryKeyExpression = x => x.Id;
                            }
                            else if (operation is BulkOperation<Meme>)
                            {
                                var bulk = (BulkOperation<Meme>)operation;
                                bulk.InsertIfNotExists = true;
                                bulk.ColumnPrimaryKeyExpression = x => x.Id;
                            }
                            operation.IncludeGraph = true;
                        };
                    });

                    await context.SaveChangesAsync();

                    return dbFolders.Select(x => x.ConvertFolder()).ToList();
                }
                else
                    throw new ArgumentNullException($"Не существует папки с guid({parentFolder?.Id})");

            } //D:\\MemeFolder\Новая папка (3)\test1\test2\1.png
        }

        #region Конструкторы

        public ExtentionalDataService()
        {
            _contextFactory = new MemeFolderNDbContextFactory();
        }

        public ExtentionalDataService(MemeFolderNDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #endregion
    }
}
