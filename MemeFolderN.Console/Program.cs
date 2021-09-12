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

namespace MemeFolderN.Console
{
    class Program
    {
        private static MemeFolderNDbContextFactory memeFolderNDbContextFactory = new MemeFolderNDbContextFactory();
        
        static void Main(string[] args)
        {
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

           


            System.Console.ReadKey();
        }

        

    }

  
}


//Folder folder = context.Folders
//    .Select(f => new Folder { Id = f.Id, Title = f.Title, Description = f.Description})
//    .FirstOrDefault(f => f.Id == Guid.Parse("52347510-A42B-4C53-B331-61A9E38A861F"));