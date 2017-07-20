using System.Data.Entity;
using Domain.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using Data.Configuration;

namespace Data.Context
{
    public class ForumContext : IdentityDbContext<User>
    {
        public ForumContext() : base("name=ForumContext")
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
