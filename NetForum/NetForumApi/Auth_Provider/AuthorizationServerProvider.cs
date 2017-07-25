using Exceptions.Validation;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UoW.Interface;
using UserAccountFacade.Interface;
using Viewmodels.UserAccount;

namespace NetForumApi
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly ILoginFacade _loginFacade;
        private readonly IUnitOfWork _uow;

        public AuthorizationServerProvider(ILoginFacade loginFacade, IUnitOfWork uow)
        {
            _loginFacade = loginFacade;
            _uow = uow;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context) => context.Validated();

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var claims = new ClaimsIdentity(context.Options.AuthenticationType);
            
            try
            {
                var user = await _loginFacade.Validate(context.UserName, context.Password);
                var userRole = _uow.GetUserRoleById(user.Id);

                SetClaims(claims, userRole, context);
                var authProperties = SetAuthenticationProperties(userRole, user);
                var ticket = new AuthenticationTicket(claims, authProperties);
                context.Validated(ticket);
            }
            catch (ServerValidationException ex)
            {
                context.SetError("invalid_grant", ex.Message);
                return;
            }
        }

        public void SetClaims(ClaimsIdentity claims, string userRole, OAuthGrantResourceOwnerCredentialsContext context)
        {
            claims.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            claims.AddClaim(new Claim(ClaimTypes.Role, userRole));
        }

        public AuthenticationProperties SetAuthenticationProperties(string userRole, UserViewmodel user)
        {
            return new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                    "userRole", userRole
                },
                {
                    "userId", user.Id
                },
                {
                    "userName", user.UserName
                }
            });
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}