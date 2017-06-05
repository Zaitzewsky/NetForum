using System;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Model;
using UserAccountFacade.Interface;
using UserAccountServiceNameSpace.Interface;
using Viewmodels.UserAccount;
using Exceptions.Validation;

namespace UserAccountFacade.Facade
{
    public class LoginFacade : ILoginFacade
    {
        private ILoginService _loginService;
        private IMapper _mapper;

        public LoginFacade(ILoginService loginService, IMapper mapper)
        {
            _loginService = loginService;
            _mapper = mapper;
        }

        public UserViewmodel MapUserViewModelFromUser(User user)
        {
            return _mapper.Map<User, UserViewmodel>(user);
        }

        public async Task<UserViewmodel> Validate(string userName, string password)
        {
            try
            {
                var user = await _loginService.Validate(userName, password);
                return MapUserViewModelFromUser(user);
            }
            catch (ServerValidationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
