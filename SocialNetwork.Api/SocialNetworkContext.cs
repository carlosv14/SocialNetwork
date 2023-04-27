using System;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Api.Models;

namespace SocialNetwork.Api
{
	public class SocialNetworkContext : DbContext
	{
        public SocialNetworkContext(DbContextOptions<SocialNetworkContext> options)
            : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<User>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .Property(x => x.Username)
                .IsRequired();

            modelBuilder.Entity<User>()
               .Property(x => x.Name)
               .IsRequired();

            modelBuilder.Entity<User>()
               .HasMany(x => x.Posts)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);


            modelBuilder.Entity<Post>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Post>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Post>()
                .Property(x => x.Content)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId);

            modelBuilder.Entity<Comment>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Comment>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Comment>()
                .Property(x => x.Content)
                .IsRequired();

        }
    }
}

