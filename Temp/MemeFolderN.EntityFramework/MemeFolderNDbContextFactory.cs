using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MemeFolderN.EntityFramework
{
    public class MemeFolderNDbContextFactory : IDesignTimeDbContextFactory<MemeFolderNDbContext>
    {
        public MemeFolderNDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<MemeFolderNDbContext>();
            options.EnableSensitiveDataLogging(true);
            options.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MemeFolderNDB;Integrated Security=True;" +
           "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //#if DEBUG

            //#else
            //            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            //            if (!Directory.Exists($@"{appData}\MemeFolder"))
            //                Directory.CreateDirectory($@"{appData}\MemeFolder");
            //            options.UseSqlite($@"Data Source={appData}\MemeFolder\MF_DB.db; Cache=Shared;");
            //#endif

            return new MemeFolderNDbContext(options.Options);
        }
    }
}

