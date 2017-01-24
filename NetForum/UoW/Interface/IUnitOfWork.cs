using Domain.Interface;

namespace UoW.Interface
{
    public interface IUnitOfWork
    {
        void Commit();
        IUserRepository GetUserRepository();
    }
}
