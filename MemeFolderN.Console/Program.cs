using MemeFolderN.Core.Models;
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

            //    MemeDataService memeDataService = new MemeDataService();
            //    Meme newMeme = new Meme()
            //    {
            //        Title = "test1",
            //        ImagePath = "test1Path"
            //    };

            //    // Act
            //    Meme dbCreatedMeme = memeDataService.Create(newMeme).Result;

            //}

        }
    }
}
