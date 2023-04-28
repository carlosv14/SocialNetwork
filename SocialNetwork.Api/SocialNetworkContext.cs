﻿using System;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Api.DatabaseConfiguration;
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
            modelBuilder.ApplyConfiguration<User>(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration<Comment>(new CommentEntityConfiguration());
            modelBuilder.ApplyConfiguration<Post>(new PostEntityConfiguration());
        }
    }
}

