namespace Data.Migrations
{
    using Context;
    using Domain.Model;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ForumContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ForumContext context)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(role => role.Name == "Admin"))
            {
                var adminRole = new IdentityRole { Name = "Admin" };
                roleManager.Create(adminRole);
            }

            if (!context.Roles.Any(role => role.Name == "ForumUser"))
            {
                var ForumUserRole = new IdentityRole { Name = "ForumUser" };
                roleManager.Create(ForumUserRole);
            }

            User admin = new User
            {
                UserName = "Admin",
                Email = "admin@netForum.com",
                PhoneNumber = "0739660278",
                PasswordHash = new PasswordHasher().HashPassword("Admin123"),
                FirstName = "Adam",
                LastName = "Adminston"
            };

            User forumUser = new User
            {
                UserName = "Zaitzewsky",
                Email = "jaqutsii@hotmail.com",
                PhoneNumber = "0739660279",
                PasswordHash = new PasswordHasher().HashPassword("Zaitzewsky12345"),
                FirstName = "Jacinto",
                LastName = "Zaitzewsky"
            };

            context.Users.AddOrUpdate(user => user.UserName,
                admin,
                forumUser);

            //This needs to be commented out at first initialization of the database.
            //After the first initialization this codesnippet underneath can be uncommented.
            if (!userManager.IsInRole(admin.Id, "Admin"))
                userManager.AddToRole(admin.Id, "Admin");

            if (!userManager.IsInRole(forumUser.Id, "ForumUser"))
                userManager.AddToRole(forumUser.Id, "ForumUser");


        }
    }
}
