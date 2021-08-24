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
                    Folder folder1 = new Folder() { ParentFolderId = context.RootGuid, Title = "Folder1" };
                    Folder folder2 = new Folder() { ParentFolderId = context.RootGuid, Title = "Folder2" };

                    FolderDTO f1 = folderDataService.Add(folder1.ConvertFolder()).Result;
                    FolderDTO f2 = folderDataService.Add(folder2.ConvertFolder()).Result;

                    FolderDTO fRup = folderDataService.Update(folder.Id, folder).Result;
                    FolderDTO fRget = folderDataService.GetById(context.RootGuid).Result;
                }
                    
                System.Console.ReadKey();
            }
            
        }
    }
}
