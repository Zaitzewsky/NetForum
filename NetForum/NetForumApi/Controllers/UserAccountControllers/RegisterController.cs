using AutoMapper;
using System.Web.Http;
using UoW.Interface;
using UserAccountFacade.Interface;
using UserAccountServiceNameSpace.Interface;
using Viewmodels.UserAccount;
using System;
using System.Threading.Tasks;
using Exceptions.Validation;
using MessageBuilder;

namespace NetForumApi.Controllers.UserAccountControllers
{
    public class RegisterController : ApiController
    {
        private readonly IUnitOfWork _uow;
        private IMapper _automapper;
        private readonly IRegisterService _registerService;
        private readonly IRegisterFacade _registerFacade;

        public RegisterController(IUnitOfWork uow, IMapper automapper, IRegisterService registerService, IRegisterFacade registerFacade)
        {
            _uow = uow;
            _automapper = automapper;
            _registerService = registerService;
            _registerFacade = registerFacade;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _registerFacade.Dispose();
                _automapper = null;
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(UserViewmodel user)
        {
            try
            {
                var identityResult = await _registerFacade.Register(user, user.Password);
                if (identityResult.Succeeded)
                    return Ok("Registration successful!");
                else
                    return BadRequest(ErrorMessageBuilder.BuildErrorMessage("Registration failed: ", identityResult.Errors));
            }
            catch (ServerValidationException serverExc)
            {
                return BadRequest(serverExc.Message);
            }
            catch(Exception ex)
            {
                return BadRequest($"Something unexpected happened: {ex.Message}. Try to reload this page.");
            }
            
        }
    }
}