using Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserAccountServiceNameSpace.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetAsync(User user);
    }
}
