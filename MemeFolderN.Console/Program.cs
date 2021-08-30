using MemeFolderN.Core.DTOClasses;
using MemeFolderN.Core.Models;
using MemeFolderN.Core.Converters;
using MemeFolderN.EntityFramework;
using MemeFolderN.EntityFramework.Services;
using System;
using MemeFolderN.Extentions.Services;

namespace MemeFolderN.Console
{
    class Program
    {
        private static MemeFolderNDbContextFactory memeFolderNDbContextFactory = new MemeFolderNDbContextFactory();
        
        static void Main(string[] args)
        {
            using (MemeFolderNDbContext context = memeFolderNDbContextFactory.CreateDbContext(null))
            {
                //System.Console.ReadKey();
            }

            UserSettingsService userSettingsService = new UserSettingsService();

            string path = userSettingsService.RootFolderPath;
            userSettingsService.RootFolderPath = "temp";
            path = userSettingsService.RootFolderPath;


            System.Console.ReadKey();
        }



    }
}
