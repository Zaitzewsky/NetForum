using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using UserAccountFacade.Interface;
using Viewmodels.UserAccount;
using UserAccountServiceNameSpace.Interface;
using Exceptions.Validation;
using AutoMapper;
using Domain.Model;

namespace UserAccountFacade.Facade
{
    public class RegisterFacade : IRegisterFacade, IDisposable
    {
        private readonly IRegisterService _registerService;
        private readonly IMapper _mapper;
        public RegisterFacade(IRegisterService registerService, IMapper mapper)
        {
            _registerService = registerService;
            _mapper = mapper;
        }

        public void Dispose()
        {
            _registerService.Dispose();
        }

        public async Task<IdentityResult> Register(UserViewmodel userViewmodel, string password)
        {
            try
            {
                var user = MapUserFromUserViewModel(userViewmodel);
                return await _registerService.Register(user, password);
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

        public User MapUserFromUserViewModel(UserViewmodel userViewmodel)
        {
            return _mapper.Map<UserViewmodel, User>(userViewmodel);
        }
    }
}
