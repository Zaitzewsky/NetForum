using Domain.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataSupplier
{
    public static class DataSupplier
    {
        public static IEnumerable<User> CreateUsers(string password)
        {
            var users = new List<User>();

            users.Add(new User
            {
                UserName = "UserName",
                Email = "user@hotmail.com",
                PhoneNumber = "0739660279",
                PasswordHash = new PasswordHasher().HashPassword(password),
                FirstName = "User",
                LastName = "Usersson"
            });
            users.Add(new User
            {
                UserName = "SecondUser",
                Email = "userTwo@hotmail.com",
                PhoneNumber = "073970279",
                PasswordHash = new PasswordHasher().HashPassword(password),
                FirstName = "UserTwo",
                LastName = "Userssonson"
            });

            users.Add(new User
            {
                UserName = "ThirdUser",
                Email = "userThree@hotmail.com",
                PhoneNumber = "0739670279",
                PasswordHash = new PasswordHasher().HashPassword(password),
                FirstName = "Use0Threer",
                LastName = "Userssonsonson"
            });

            return users;
        }
    }
}
