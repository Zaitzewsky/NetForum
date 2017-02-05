using Domain.Interface;
using System;

namespace UoW.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        IUserRepository GetUserRepository();
        new void Dispose();
    }
}
