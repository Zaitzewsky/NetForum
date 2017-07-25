using Domain.Interface;
using System;

namespace UoW.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        IUserRepository GetUserRepository();
        string GetUserRoleById(string userId);
        new void Dispose();
        
    }
}
