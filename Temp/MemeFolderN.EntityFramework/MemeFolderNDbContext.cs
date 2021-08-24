using MemeFolderN.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MemeFolderN.EntityFramework
{
    public class MemeFolderNDbContext : DbContext
    {
        public DbSet<Meme> Memes { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<MemeTag> MemeTags { get; set; }
        public DbSet<MemeTagNode> MemeTagNodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Folder>(entity => {

                entity.HasOne(f => f.ParentFolder);
                entity.HasMany(f => f.Folders);
                entity.HasMany(f => f.Memes);
            });


            modelBuilder.Entity<Meme>(entity =>
            {
                entity.HasOne(m => m.ParentFolder);
                entity.HasMany(m => m.Tags);    
            });

            modelBuilder.Entity<MemeTagNode>(entity =>
            {
                entity.HasOne(m => m.MemeTag);
                entity.HasOne(m => m.Meme);
            });


        }

        public MemeFolderNDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
