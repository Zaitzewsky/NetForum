using System.Data.Entity;
using Domain.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using Data.Configuration;

namespace Data.Context
{
    public class ForumContext : IdentityDbContext<User>
    {
        public ForumContext() : base("Data Source=DESKTOP-CB5546T;Initial Catalog=NetForum;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UserConfiguration());

            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
        }
    }
}
