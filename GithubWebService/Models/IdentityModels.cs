using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

namespace GithubWebService.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("GithubConnection", throwIfV1Schema: false)
    //    {
    //    }
    //    public DbSet<User> User { get; set; }

    //    public DbSet<Repository> Repository { get; set; }

    //    public DbSet<Organization> Organization { set; get; }

    //    public DbSet<OrganizationMember> OrganizationMember { set; get; }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<Repository>()
    //        .HasRequired<User>(s => s.RepUser)
    //        .WithMany(g => g.RepositoryList)
    //        .HasForeignKey(s => s.UserId);

    //        modelBuilder.Entity<OrganizationMember>()
    //        .HasRequired<Organization>(s => s.Organization)
    //        .WithMany(g => g.OrganizationMemberList)
    //        .HasForeignKey(s => s.OrganizationID);

    //        modelBuilder.Entity<OrganizationMember>()
    //        .HasRequired<User>(s => s.User)
    //        .WithMany()
    //        .HasForeignKey(s => s.UserID);
    //    }

    //    public static ApplicationDbContext Create()
    //    {

    //        return new ApplicationDbContext();
    //    }
    //}
}