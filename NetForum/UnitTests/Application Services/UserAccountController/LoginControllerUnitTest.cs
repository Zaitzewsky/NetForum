using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserAccountFacade.Interface;
using Moq;
using NetForumApi.Controllers.UserAccountControllers;
using Viewmodels.UserAccount;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using System.Web.Http.Results;
using System.Web.Http;
using Exceptions.Validation;
using MessageBuilder;

namespace UnitTests.Application_Services.UserAccountController
{
    [TestClass]
    public class LoginControllerUnitTest
    {
        private Mock<ILoginFacade> _loginFacadeMock;
        private LoginController _sut;
        private UserViewmodel _user;

        #region Setup
        [TestInitialize]
        public void Initialize()
        {
            _user = DataSupplier.DataSupplier.CreateUserViewmodels("Password").First();

            _loginFacadeMock = new Mock<ILoginFacade>();
            _loginFacadeMock.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(_user));

            _sut = new LoginController(_loginFacadeMock.Object);
        }
        #endregion

        #region Tests

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task LoginControllerPostReturns200()
        {
            var httpActionResult = await _sut.Post(_user);

            httpActionResult.Should().BeOfType<OkNegotiatedContentResult<UserViewmodel>>();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task LoginControllerPostReturns400()
        {
            string[] errors = { "Username already exist." };
            string loginErrorMessage = "Login failed due to these issues: ";
            
            IHttpActionResult httpActionResult;

            _loginFacadeMock.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<string>())).Throws(new ServerValidationException(ErrorMessageBuilder.BuildErrorMessage(loginErrorMessage, errors), ServerValidationException.ServerValidationExceptionType.Error));
            httpActionResult = await _sut.Post(_user);
            httpActionResult.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task LoginControllerPostReturns400WithCorrectErrorMessageServerValidationException()
        {
            string[] errors = { "Username already exist." };
            var loginErrorMessage = "Login failed due to these issues: ";
            var spaces = "\r\n\r\n";

            IHttpActionResult httpActionResult;

            _loginFacadeMock.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<string>())).Throws(new ServerValidationException(ErrorMessageBuilder.BuildErrorMessage(loginErrorMessage, errors), ServerValidationException.ServerValidationExceptionType.Error));
            httpActionResult = await _sut.Post(_user);
            var badRequest = httpActionResult as BadRequestErrorMessageResult;

            badRequest.Message.Should().BeEquivalentTo(loginErrorMessage + spaces + errors[0]);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task LoginControllerPostReturns400WithCorrectErrorMessageGeneralException()
        {
            var error = "Exception thrown!";
            var fullErrorMessage = $"Something unexpected happened: {error}. Try to reload this page.";
            
            _loginFacadeMock.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception(error));
            var httpActionResult = await _sut.Post(_user);
            var badRequest = httpActionResult as BadRequestErrorMessageResult;

            badRequest.Message.Should().BeEquivalentTo(fullErrorMessage);
        }

        #endregion
    }
}
