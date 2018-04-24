using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GithubWebApi.Models
{
    public class GithubDataContext : DbContext
    {
        public GithubDataContext(): base("GithubConnection") {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<User> User { get; set; }

        public DbSet<Repository> Repository { get; set; }

        public DbSet<Organization> Organization { set; get; }

        public DbSet<OrganizationMember> OrganizationMember { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Repository>()
            .HasRequired<User>(s => s.RepUser)
            .WithMany(g => g.RepositoryList)
            .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<OrganizationMember>()
            .HasRequired<Organization>(s => s.Organization)
            .WithMany(g => g.OrganizationMemberList)
            .HasForeignKey(s => s.OrganizationID);

            modelBuilder.Entity<OrganizationMember>()
            .HasRequired<User>(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserID);
        }
        public static GithubDataContext Create()
        {

            return new GithubDataContext();
        }
    }
}