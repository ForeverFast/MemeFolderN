using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.IO;

namespace MemeFolderN.EntityFramework
{
    public class MemeFolderNDbContextFactory : IDesignTimeDbContextFactory<MemeFolderNDbContext>
    {
        public MemeFolderNDbContext CreateDbContext(string[] args)
        {

            var options = new DbContextOptionsBuilder<MemeFolderNDbContext>();
            options.EnableSensitiveDataLogging(true);

            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!Directory.Exists($@"{appData}\MemeFolder"))
                Directory.CreateDirectory($@"{appData}\MemeFolder");
            options.UseSqlite($@"Data Source={appData}\MemeFolder\MFN_DB.db; ");/*Cache=Shared;*/

            // options.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MemeFolderNDB;Integrated Security=True;" +
            //"Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //#if DEBUG

            //#else
            //          
            //#endif

            return new MemeFolderNDbContext(options.Options);
        }
    }
}

