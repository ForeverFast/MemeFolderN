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
            //using (MemeFolderNDbContext context = memeFolderNDbContextFactory.CreateDbContext(null))
            //{
            //    System.Console.ReadKey();
            //}

            Folder folder1 = new Folder() { Id = 1, Title = "T1", Descrp = "D1" };
            Folder folder2 = folder1 with { Title = "T2" };

            Folder folder3 = folder1 with { ParentFolder = folder2, Title = "T3" };
            folder3 = folder3 with { ParentFolder = null, Title = "T3" };

            System.Console.ReadKey();
        }



    }


    record Folder
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Descrp { get; set; }
        public Folder ParentFolder { get; init; }
    }
}
