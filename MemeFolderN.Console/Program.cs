using MemeFolderN.Core.DTOClasses;
using MemeFolderN.Core.Models;
using MemeFolderN.Core.Converters;
using MemeFolderN.EntityFramework;
using MemeFolderN.EntityFramework.Services;
using System;
using MemeFolderN.Extentions.Services;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemeFolderN.Extentions;
using Z.EntityFramework.Extensions;
using Z.BulkOperations;
using System.IO;

namespace MemeFolderN.Console
{
    class Program
    {
        private static MemeFolderNDbContextFactory memeFolderNDbContextFactory = new MemeFolderNDbContextFactory();

        static void Main(string[] args)
        {
            Folder folder = new Folder { Title = "123" };

            Func<object> func = () => folder;

            object obj = func.Invoke();

            return;
         
            try
            {
                EntityFrameworkManager.ContextFactory = context => memeFolderNDbContextFactory.CreateDbContext(null);

                var t = Path.GetExtension(@"D:\Пикчи\Картинки\Арты\10190.jpg");

                Folder folderR = new Folder()
                {
                    Title = "folderRoot",
                    Folders = new List<Folder>()
                {
                     new Folder()
                     {
                         Title = "folder1",
                         Folders = new List<Folder>()
                         {
                             new Folder()
                             {
                                Title = "folder11"
                             },
                             new Folder()
                             {
                                Title = "folder12",

                             }
                         },
                         Memes = new List<Meme>()
                        {
                            new Meme()
                            {
                                Title = "Meme11"
                            },
                             new Meme()
                            {
                                Title = "Meme12"
                            }
                        }

                     },
                     new Folder()
                     {
                         Title = "folder2",
                         Folders = new List<Folder>()
                         {
                              new Folder()
                             {
                                Title = "folder21"
                             },
                             new Folder()
                             {
                                Title = "folder22",

                             }
                         },
                          Memes = new List<Meme>()
                        {
                            new Meme()
                            {
                                Title = "Meme21"
                            },
                             new Meme()
                            {
                                Title = "Meme22"
                            }
                        }
                     }
                },
                    Memes = new List<Meme>()
                {
                    new Meme()
                    {
                        Title = "MemeRoot1"
                    },
                     new Meme()
                    {
                        Title = "MemeRoot2"
                    }
                }
                };


                using (MemeFolderNDbContext context = memeFolderNDbContextFactory.CreateDbContext(null))
                {
                    //Folder folder = context.Folders.FirstOrDefault(x => x.Title == "folderRoot");

                    //folder.Memes = new List<Meme>() { new Meme() { Title = "newRootMeme1" } };

                    ////context.BulkInsert(new List<Folder>() { folder },opt => opt.IncludeGraph = true);

                    //context.BulkInsert(new List<Folder>() { folder }, options =>
                    //{
                    //    options.IncludeGraphOperationBuilder = operation =>
                    //    {
                    //        if (operation is BulkOperation<Folder>)
                    //        {
                    //            var bulk = (BulkOperation<Folder>)operation;
                    //            bulk.InsertIfNotExists = true;
                    //            bulk.ColumnPrimaryKeyExpression = x => x.Id;
                    //        }
                    //        else if (operation is BulkOperation<Meme>)
                    //        {
                    //            var bulk = (BulkOperation<Meme>)operation;
                    //            bulk.InsertIfNotExists = true;
                    //            bulk.ColumnPrimaryKeyExpression = x => x.Id;
                    //        }
                    //        operation.IncludeGraph = true;
                    //    };

                    //});
                    //context.SaveChanges();


                    //var m1 = context.Memes.FirstOrDefault(x => x.Title == "MemeRoot1");
                    //var m2 = context.Memes.FirstOrDefault(x => x.Title == "MemeRoot2");

                    //var mt1 = context.MemeTags.FirstOrDefault(x => x.Title == "tag3");
                    //var mt2 = context.MemeTags.FirstOrDefault(x => x.Title == "tag2");

                    //var mtn1 = new MemeTagNode { MemeId = m1.Id, MemeTagId = mt1.Id };
                    //var mtn2 = new MemeTagNode { MemeId = m2.Id, MemeTagId = mt2.Id };


                    //context.MemeTagNodes.AddRange(mtn1, mtn2);

                    //context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Console.ReadKey();
            }


        }



    }


}
/*
 IEnumerable<Folder> entities = null;
            List<FolderDTO> entitiesDTO = null;
            List<FolderDTO> result = new List<FolderDTO>();
            using (MemeFolderNDbContext context = memeFolderNDbContextFactory.CreateDbContext(null))
            {

                entities = Task.FromResult(context.Folders
                   .Include(f => f.Memes)
                   .ToList()).Result;
                entitiesDTO = entities.Where(f=>f.ParentFolderId == null).Select(f => f.ConvertFolder()).ToList();

                entitiesDTO.ForEach(f =>
                {
                    result.Add(f);
                    result.AddRange(DataExtentions.SelectRecursive(f.Folders, innerF => innerF.Folders));
                });
            }



 */

//Folder folder = context.Folders
//    .Select(f => new Folder { Id = f.Id, Title = f.Title, Description = f.Description})
//    .FirstOrDefault(f => f.Id == Guid.Parse("52347510-A42B-4C53-B331-61A9E38A861F"));