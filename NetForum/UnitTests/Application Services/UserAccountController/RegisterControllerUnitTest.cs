using UoW.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UserAccountFacade.Interface;
using Viewmodels.UserAccount;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using AutoMapper;
using UserAccountServiceNameSpace.Interface;
using NetForumApi.Controllers.UserAccountControllers;
using System.Linq;
using System.Web.Http.Results;
using System;

namespace UnitTests.Application_Services.UserAccountController
{
    [TestClass]
    public class RegisterControllerUnitTest
    {
        private Mock<IRegisterFacade> _registerFacadeMock;
        private Mock<IRegisterService> _registerServiceMock;
        private Mock<IUnitOfWork> _uowMock;
        private Mock<IMapper> _automapperMock;
        private RegisterController _sut;

        private UserViewmodel _user;

        #region Setup
        [TestInitialize]
        public void Initialize()
        {
            _user = DataSupplier.DataSupplier.CreateUserViewmodels("Password").First();

            _registerFacadeMock = new Mock<IRegisterFacade>();
            _automapperMock = new Mock<IMapper>();
            _registerServiceMock = new Mock<IRegisterService>();
            _uowMock = new Mock<IUnitOfWork>();

            _registerFacadeMock.Setup(x => x.Register(It.IsAny<UserViewmodel>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));

            _sut = new RegisterController(_uowMock.Object, _automapperMock.Object, _registerServiceMock.Object, _registerFacadeMock.Object);
        }
        #endregion

        #region Tests
        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task RegisterControllerReturns200()
        {
            var httpActionResult = await _sut.Post(_user);

            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<string>));
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task RegisterControllerReturns400()
        {
            string[] errors = new string[0];
            _registerFacadeMock.Setup(x => x.Register(It.IsAny<UserViewmodel>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed(errors)));
            var httpActionResult = await _sut.Post(_user);

            Assert.IsInstanceOfType(httpActionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task RegisterControllerReturns400WithCorrectErrorMessage()
        {
            var error = "Username already exists!";
            var errorMessage = "Registration failed: " + Environment.NewLine + Environment.NewLine + error;

            string[] errors = new string[] { error };
            _registerFacadeMock.Setup(x => x.Register(It.IsAny<UserViewmodel>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed(errors)));
            var httpActionResult = await _sut.Post(_user);
            var badRequest = httpActionResult as BadRequestErrorMessageResult;

            Assert.AreEqual(errorMessage, badRequest.Message);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task RegisterControllerReturns400WithCorrectErrorMessageServerValidationException()
        {
            var errorMessage = "ServerValidationException thrown!";
            _registerFacadeMock.Setup(x => x.Register(It.IsAny<UserViewmodel>(), It.IsAny<string>())).Throws(new Exceptions.Validation.ServerValidationException(errorMessage));
            var httpActionResult = await _sut.Post(_user);
            var badRequest = httpActionResult as BadRequestErrorMessageResult;

            Assert.AreEqual(errorMessage, badRequest.Message);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task RegisterControllerReturns400WithCorrectErrorMessageGeneralException()
        {
            var fullErrorMessage = "Something unexpected happened: Exception thrown!. Try to reload this page.";
            var error = "Exception thrown!";
            _registerFacadeMock.Setup(x => x.Register(It.IsAny<UserViewmodel>(), It.IsAny<string>())).Throws(new Exception(error));
            var httpActionResult = await _sut.Post(_user);
            var badRequest = httpActionResult as BadRequestErrorMessageResult;

            Assert.AreEqual(fullErrorMessage, badRequest.Message);
        }
        #endregion
    }
}
