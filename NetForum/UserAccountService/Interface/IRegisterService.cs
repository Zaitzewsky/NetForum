using Domain.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace UserAccountServiceNameSpace.Interface
{
    public interface IRegisterService : IDisposable
    {
        Task<IdentityResult> Register(User _user, string _password);
        Task<IdentityResult> SetForumUserRole(User _user);
    }
}
