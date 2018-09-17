using Microsoft.AspNet.Identity.EntityFramework;
using SocialNetwork.Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Database.Contexts
{
    public class SocialNetworkContext : IdentityDbContext<ApplicationUser>
    {
        public SocialNetworkContext()
            : base("SocialNetworkConnection")
        {
            this.Database.Log = (s) => Debug.Write(s);
        }

        public DbSet<User> SocialNetworkUsers { get; set; }

        public DbSet<Friendship> Friendships { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Reaction> Reactions { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Properties<DateTime>().Configure(t => t.HasPrecision(6));
            modelBuilder.Properties<string>().Configure(t => t.IsUnicode(false));
            this.ConfigurePosts(modelBuilder.Entity<Post>());
        }

        private void ConfigurePosts(EntityTypeConfiguration<Post> entityTypeConfiguration)
        {
            entityTypeConfiguration
                .HasMany(p => p.Comments)
                .WithRequired(c => c.Post)
                .HasForeignKey(c => c.PostId);

            entityTypeConfiguration
                .HasMany(p => p.Reactions)
                .WithRequired(r => r.Post);
        }
    }
}
