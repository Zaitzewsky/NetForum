using Data.Context;
using Data.Repository.Repository;
using Domain.Interface;
using System;
using UoW.Interface;

namespace UoW
{
    /// <summary>
    /// This will be used in the business layer only. The business layer will use this class in a using statement.
    /// This is so that the DbContext will automatically be disposed.
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ForumContext _context;
        private IUserRepository _userRepository;

        public UnitOfWork()
        {
            _context = new ForumContext();
        }

        public IUserRepository GetUserRepository()
        {
            return _userRepository ?? (_userRepository = new UserRepository(_context));
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            if (_context == null)
                return;
        }
    }
}
