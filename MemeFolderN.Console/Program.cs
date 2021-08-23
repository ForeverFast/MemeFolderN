using MemeFolderN.Core.DTOClasses;
using MemeFolderN.Core.Models;
using MemeFolderN.Core.Converters;
using MemeFolderN.EntityFramework;
using MemeFolderN.EntityFramework.Services;
using System;

namespace MemeFolderN.Console
{
    class Program
    {
        private static MemeFolderNDbContextFactory memeFolderNDbContextFactory = new MemeFolderNDbContextFactory();
        
        static void Main(string[] args)
        {
           

            using (MemeFolderNDbContext context = memeFolderNDbContextFactory.CreateDbContext(null))
            {

                FolderDataService folderDataService = new FolderDataService();
                FolderDTO folder = folderDataService.GetById(context.RootGuid).Result;
                if (folder != null)
                {
                    Folder folder1 = new Folder() { ParentFolderId = context.RootGuid };

                    var t = folderDataService.Add(folder1.ConvertFolder()).Result;
                }
                    

                System.Console.ReadKey();
            }
            
        }
    }
}
