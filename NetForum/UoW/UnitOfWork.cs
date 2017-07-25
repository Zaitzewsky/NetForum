using Data.Context;
using Data.Repository.Repository;
using Domain.Interface;
using Domain.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using UoW.Interface;

namespace UoW
{
    /// <summary>
    /// This will be used in the business layer only. The business layer will use this class in a using statement.
    /// This is so that the DbContext will automatically be disposed.
    /// Make the business layer class to implement IDisposable and then instantiate the business layer class per every
    /// facade method and then in a "finally"-block the business layer object can be disposed.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ForumContext _context;
        private readonly UserManager<User> _userManager;
        private IUserRepository _userRepository;

        public UnitOfWork()
        {
            _context = new ForumContext();
            _userManager = new UserManager<User>(new UserStore<User>(_context));
        }

        public IUserRepository GetUserRepository()
        {
            return _userRepository ?? (_userRepository = new UserRepository(_context, _userManager));
        }

        public string GetUserRoleById(string userId)
        {
            var roles = _userManager.GetRoles(userId);
            var singleRole = roles[0];
            return singleRole;
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
