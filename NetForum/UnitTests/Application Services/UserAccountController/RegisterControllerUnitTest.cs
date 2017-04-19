using Moq;
using UserAccountFacade.Interface;
using Viewmodels.UserAccount;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NetForumApi.Controllers.UserAccountControllers;
using System.Linq;
using System.Web.Http.Results;
using System;
using Xunit;
using FluentAssertions;

namespace UnitTests.Application_Services.UserAccountController
{
    public class RegisterControllerUnitTest
    {
        private readonly Mock<IRegisterFacade> _registerFacadeMock;
        private readonly RegisterController _sut;
        private readonly UserViewmodel _user;

        #region Setup
        public RegisterControllerUnitTest()
        {
            _user = DataSupplier.DataSupplier.CreateUserViewmodels("Password").First();

            _registerFacadeMock = new Mock<IRegisterFacade>();
            _registerFacadeMock.Setup(x => x.Register(It.IsAny<UserViewmodel>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));

            _sut = new RegisterController(_registerFacadeMock.Object);
        }
        #endregion

        #region Tests
        [Fact]
        [Trait("Category", "Unit")]
        public async Task RegisterControllerReturns200()
        {
            var httpActionResult = await _sut.Post(_user);

            httpActionResult.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task RegisterControllerReturns400()
        {
            string[] errors = new string[0];
            _registerFacadeMock.Setup(x => x.Register(It.IsAny<UserViewmodel>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed(errors)));
            var httpActionResult = await _sut.Post(_user);

            httpActionResult.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task RegisterControllerReturns400WithCorrectErrorMessage()
        {
            var error = "Username already exists!";
            var errorMessage = "Registration failed: " + Environment.NewLine + Environment.NewLine + error;

            string[] errors = new string[] { error };
            _registerFacadeMock.Setup(x => x.Register(It.IsAny<UserViewmodel>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed(errors)));
            var httpActionResult = await _sut.Post(_user);
            var badRequest = httpActionResult as BadRequestErrorMessageResult;

            badRequest.Message.Should().BeEquivalentTo(errorMessage);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task RegisterControllerReturns400WithCorrectErrorMessageServerValidationException()
        {
            var errorMessage = "ServerValidationException thrown!";
            _registerFacadeMock.Setup(x => x.Register(It.IsAny<UserViewmodel>(), It.IsAny<string>())).Throws(new Exceptions.Validation.ServerValidationException(errorMessage));
            var httpActionResult = await _sut.Post(_user);
            var badRequest = httpActionResult as BadRequestErrorMessageResult;

            badRequest.Message.Should().BeEquivalentTo(errorMessage);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task RegisterControllerReturns400WithCorrectErrorMessageGeneralException()
        {
            var fullErrorMessage = "Something unexpected happened: Exception thrown!. Try to reload this page.";
            var error = "Exception thrown!";
            _registerFacadeMock.Setup(x => x.Register(It.IsAny<UserViewmodel>(), It.IsAny<string>())).Throws(new Exception(error));
            var httpActionResult = await _sut.Post(_user);
            var badRequest = httpActionResult as BadRequestErrorMessageResult;

            badRequest.Message.Should().BeEquivalentTo(fullErrorMessage);
        }
        #endregion
    }
}
