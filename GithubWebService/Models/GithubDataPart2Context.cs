using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GithubWebService.Models
{
    public class GithubDataPart2Context : DbContext
    {
        public GithubDataPart2Context()
            : base("GithubServicePart2Connection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<User> User { get; set; }
        public DbSet<Repository> Repository { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Repository>()
            .HasRequired<User>(s => s.RepUser)
            .WithMany(g => g.RepositoryList)
            .HasForeignKey(s => s.UserId);

        }
        public static GithubDataPart2Context Create()
        {

            return new GithubDataPart2Context();
        }
    }
}