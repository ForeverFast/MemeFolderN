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
                System.Console.ReadKey();
            }

           

            System.Console.ReadKey();
        }



    }
}
