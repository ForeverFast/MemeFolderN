using MemeFolderN.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MemeFolderN.EntityFramework
{
    public class MemeFolderNDbContext : DbContext
    {
        public Guid RootGuid { get; } = Guid.Parse("00000000-0000-0000-0000-000000000001");

        public DbSet<Meme> Memes { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<MemeTag> MemeTags { get; set; }
        public DbSet<MemeTagNode> MemeTagNodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Folder>(entity => {

                entity.HasOne(f => f.ParentFolder)
                .WithMany(f => f.Folders)
                .IsRequired(false);
               
                
                entity.HasMany(f => f.Folders);
                
                entity.HasMany(f => f.Memes);
                //.WithOne(m => m.ParentFolder)
                //.HasForeignKey(m => m.ParentFolderId);
               
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

            //modelBuilder.Entity<Folder>().HasData(new Folder { Id = RootGuid, Title = "root" });
        }

        public MemeFolderNDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
