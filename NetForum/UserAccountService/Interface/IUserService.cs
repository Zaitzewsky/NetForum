using Domain.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserAccountServiceNameSpace.Interface
{
    public interface IUserService : IDisposable
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetAsync(User user);
        Task UpdateAsync(User _user);
    }
}
