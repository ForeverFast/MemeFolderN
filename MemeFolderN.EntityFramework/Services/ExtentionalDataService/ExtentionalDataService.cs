using AutoMapper;
using MemeFolderN.Core.DTOClasses;
using MemeFolderN.Core.Models;
using MemeFolderN.EntityFramework.AutoMapperProfiles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.BulkOperations;

namespace MemeFolderN.EntityFramework.Services
{
    public class ExtentionalDataService : IExtentionalDataService
    {
        protected readonly MemeFolderNDbContextFactory _contextFactory;
        protected readonly IMapper _mapper;

        public virtual async Task<List<FolderDTO>> BulkInsertAndUpdateFolder(FolderDTO parentFolder)
        {
            using (MemeFolderNDbContext context = _contextFactory.CreateDbContext(null))
            {
                Folder folder = await context.Folders.FirstOrDefaultAsync(x => x.Id == parentFolder.Id);

                if (folder != null)
                {
                    folder.Folders = parentFolder.Folders.Select(x => _mapper.Map<Folder>(x)).ToList();
                    folder.Memes = parentFolder.Memes.Select(x => _mapper.Map<Meme>(x)).ToList();
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

                    return dbFolders.Select(x => _mapper.Map<FolderDTO>(x)).ToList();
                }
                else
                    throw new ArgumentNullException($"Не существует папки с guid({parentFolder?.Id})");

            }
        }

        #region Конструкторы

        public ExtentionalDataService()
        {
            _contextFactory = new MemeFolderNDbContextFactory();
            _mapper = new Mapper(new MapperConfiguration(opt =>
            {
                opt.AddProfile(new MapperProfileDAL());
            }));
        }

        public ExtentionalDataService(MemeFolderNDbContextFactory contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        #endregion
    }
}
