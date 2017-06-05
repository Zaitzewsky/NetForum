using System;
using System.Threading.Tasks;
using Domain.Model;
using UoW.Interface;
using UserAccountServiceNameSpace.Interface;
using Domain.Interface;
using System.Data.Entity.Validation;
using MessageBuilder;
using Exceptions.Validation;

namespace UserAccountServiceNameSpace.Service
{
    public class LoginService : ILoginService, IDisposable
    {
        private IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;

        public LoginService(IUnitOfWork uow)
        {
            _uow = uow;
            _userRepository = _uow.GetUserRepository();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

        public async Task<User> Validate(string userName, string password)
        {
            try
            {
                return await _userRepository.Validate(userName, password);
            }
            catch (DbEntityValidationException ex)
            {
                throw new ServerValidationException(ErrorMessageBuilder.BuildErrorMessage("Login failed due to these issues: ", ex.EntityValidationErrors), ServerValidationException.ServerValidationExceptionType.Error);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
