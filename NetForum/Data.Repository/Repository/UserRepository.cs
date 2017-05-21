using Domain.Model;
using Domain.Interface;
using System;
using System.Collections.Generic;
using Data.Context;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Data.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ForumContext _context;
        private readonly UserManager<User> _userManager;
        public UserManager<User> UserManager { get { return _userManager; } }

        public UserRepository(ForumContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync().ConfigureAwait(false);
        }

        public async Task<User> GetAsync(User entity)
        {
            return await _context.Users.Where(user => user.Id == entity.Id).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<IdentityResult> Register(User user, string password)
        {
            return await _userManager.CreateAsync(user, password).ConfigureAwait(false);
        }

        public async Task<IdentityResult> UpdateAsync(User user)
        {
            return await _userManager.UpdateAsync(user).ConfigureAwait(false);
        }

        public async Task<User> Validate(string userName, string password)
        {
            return await _userManager.FindAsync(userName, password).ConfigureAwait(false);
        }

        public async Task<IdentityResult> SetForumUserRole(User user)
        {
            return await _userManager.AddToRoleAsync(user.Id, "ForumUser").ConfigureAwait(false);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        #region Unused Interface Implementation
        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User Get(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
