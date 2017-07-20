using UserAccountFacade.Interface;
using System.Collections.Generic;
using System.Web.Http;
using System;
using System.Threading.Tasks;
using Viewmodels.UserAccount;
using Exceptions.Validation;

namespace NetForumApi.Controllers.UserAccountControllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        private readonly ILoginFacade _loginFacade;

        public LoginController(ILoginFacade loginFacade)
        {
            _loginFacade = loginFacade;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _loginFacade.Dispose();

            base.Dispose(disposing);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> Post(UserViewmodel user)
        {
            try
            {
                var validatedUser = await _loginFacade.Validate(user.UserName, user.Password);

                return Ok(validatedUser);
            }
            catch (ServerValidationException serverExc)
            {
                return BadRequest(serverExc.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Something unexpected happened: {ex.Message}. Try to reload this page.");
            }
        }
    }
}