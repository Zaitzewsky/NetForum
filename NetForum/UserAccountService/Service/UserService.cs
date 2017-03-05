using Domain.Interface;
using Domain.Model;
using Exceptions.Validation;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UoW.Interface;
using UserAccountServiceNameSpace.Interface;

namespace UserAccountServiceNameSpace.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _userRepository = _uow.GetUserRepository();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                if(users == null)
                    throw new ServerValidationException("No users found.");
                if (!users.Any())
                    throw new ServerValidationException("No users found.");

                return users;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetAsync(User user)
        {
            try
            {
                var asyncUser = await _userRepository.GetAsync(user);
                if (asyncUser == null)
                    throw new ServerValidationException("No users found.");

                return asyncUser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IdentityResult> UpdateAsync(User user)
        {
            try
            {
                return await _userRepository.UpdateAsync(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
