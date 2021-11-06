using MemeFolderN.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Z.EntityFramework.Extensions;

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

                entity.HasMany(f => f.Folders)
                .WithOne(f => f.ParentFolder)
                .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(f => f.Memes)
                .WithOne(f => f.ParentFolder)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Meme>(entity =>
            {
                entity.HasOne(m => m.ParentFolder);
                entity.HasMany(m => m.TagNodes)
                .WithOne(tn => tn.Meme)
                .OnDelete(DeleteBehavior.Cascade);
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
