using Domain.Model;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IdentityResult> Register(User user, string password);
        Task<User> Validate(string userName, string password);
        Task<IdentityResult> UpdateAsync(User user);
        Task<IdentityResult> SetForumUserRole(User user);
        UserManager<User> UserManager { get; }
    }
}
