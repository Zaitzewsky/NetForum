using System;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.AspNet.Identity;
using UoW.Interface;
using UserAccountServiceNameSpace.Interface;
using Domain.Interface;
using Exceptions.Validation;
using MessageBuilder;
using System.Linq;
using System.Data.Entity.Validation;

namespace UserAccountServiceNameSpace.Service
{
    public class RegisterService : IRegisterService, IDisposable
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;

        public RegisterService(IUnitOfWork uow)
        {
            _uow = uow;
            _userRepository = _uow.GetUserRepository();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

        public async Task<IdentityResult> Register(User user, string password)
        {
            try
            {
                return await _userRepository.Register(user, password);
            }
            catch (DbEntityValidationException ex)
            {
                throw new ServerValidationException(ErrorMessageBuilder.BuildErrorMessage("Registration failed due to these issues: ", ex.EntityValidationErrors), ServerValidationException.ServerValidationExceptionType.Error);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
