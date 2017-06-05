using System.Threading.Tasks;
using Domain.Model;
using System;

namespace UserAccountServiceNameSpace.Interface
{
    public interface ILoginService : IDisposable
    {
        Task<User> Validate(string userName, string password);
    }
}
