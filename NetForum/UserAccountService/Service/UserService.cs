using Domain.Interface;
using Domain.Model;
using Exceptions.Validation;
using MessageBuilder;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetUserRepository();
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

        public async Task UpdateAsync(User user)
        {
            try
            {
                var identityResult = await _userRepository.UpdateAsync(user);
                if (identityResult.Succeeded)
                    throw new ServerValidationException("Update successful!", ServerValidationException.ServerValidationExceptionType.Success);

                if (!identityResult.Succeeded || (identityResult.Errors.Any() && !identityResult.Succeeded))
                    throw new ServerValidationException(ErrorMessageBuilder.BuildErrorMessage("Update failed due to these issues: ", identityResult.Errors), ServerValidationException.ServerValidationExceptionType.Error);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
