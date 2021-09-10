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

namespace MemeFolderN.Console
{
    class Program
    {
        private static MemeFolderNDbContextFactory memeFolderNDbContextFactory = new MemeFolderNDbContextFactory();
        
        static void Main(string[] args)
        {
            using (MemeFolderNDbContext context = memeFolderNDbContextFactory.CreateDbContext(null))
            {
                Folder folder = context.Folders
                    .Select(f => new Folder { Id = f.Id, Title = f.Title, Description = f.Description})
                    .FirstOrDefault(f => f.Id == Guid.Parse("52347510-A42B-4C53-B331-61A9E38A861F"));


                System.Console.ReadKey();
            }

           


            System.Console.ReadKey();
        }

        

    }

    [Table("Folders")]
    class IncompleteFolder
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
    }
}
