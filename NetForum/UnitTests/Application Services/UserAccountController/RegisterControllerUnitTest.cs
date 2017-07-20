using Moq;
using UserAccountFacade.Interface;
using Viewmodels.UserAccount;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NetForumApi.Controllers.UserAccountControllers;
using System.Linq;
using System.Web.Http.Results;
using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Application_Services.UserAccountController
{
    [TestClass]
    public class RegisterControllerUnitTest
    {
        private Mock<IRegisterFacade> _registerFacadeMock;
        private RegisterController _sut;
        private UserViewmodel _user;

        #region Setup
        [TestInitialize]
        public void Initialize()
        {
            _user = DataSupplier.DataSupplier.CreateUserViewmodels("Password").First();

            _registerFacadeMock = new Mock<IRegisterFacade>();
            _registerFacadeMock.Setup(x => x.Register(It.IsAny<UserViewmodel>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));

            _sut = new RegisterController(_registerFacadeMock.Object);
        }
        #endregion

        #region Tests
        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task RegisterControllerReturnsPost200()
        {
            var httpActionResult = await _sut.Post(_user);

            httpActionResult.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task RegisterControllerReturnsPost400()
        {
            string[] errors = new string[0];
            _registerFacadeMock.Setup(x => x.Register(It.IsAny<UserViewmodel>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed(errors)));
            var httpActionResult = await _sut.Post(_user);

            httpActionResult.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task RegisterControllerReturnsPost400WithCorrectErrorMessage()
        {
            var error = "Username already exists!";
            var errorMessage = "Registration failed: " + Environment.NewLine + Environment.NewLine + error;

            string[] errors = new string[] { error };
            _registerFacadeMock.Setup(x => x.Register(It.IsAny<UserViewmodel>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed(errors)));
            var httpActionResult = await _sut.Post(_user);
            var badRequest = httpActionResult as BadRequestErrorMessageResult;

            badRequest.Message.Should().BeEquivalentTo(errorMessage);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task RegisterControllerReturnsPost400WithCorrectErrorMessageServerValidationException()
        {
            var errorMessage = "ServerValidationException thrown!";
            _registerFacadeMock.Setup(x => x.Register(It.IsAny<UserViewmodel>(), It.IsAny<string>())).Throws(new Exceptions.Validation.ServerValidationException(errorMessage));
            var httpActionResult = await _sut.Post(_user);
            var badRequest = httpActionResult as BadRequestErrorMessageResult;

            badRequest.Message.Should().BeEquivalentTo(errorMessage);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task RegisterControllerPostReturns400WithCorrectErrorMessageGeneralException()
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
