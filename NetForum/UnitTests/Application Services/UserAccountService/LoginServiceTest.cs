using Moq;
using System.Threading.Tasks;
using System.Linq;
using FluentAssertions;
using UoW.Interface;
using Domain.Interface;
using Domain.Model;
using UserAccountServiceNameSpace.Interface;
using UserAccountServiceNameSpace.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity.Validation;
using Exceptions.Validation;

namespace UnitTests.Application_Services.UserAccountService
{
    [TestClass]
    public class LoginServiceTest
    {
        private Mock<IUnitOfWork> _uow;
        private Mock<IUserRepository> _userRepository;
        private ILoginService _sut;
        private User _user;
        private string _password = "Password";

        [TestInitialize]
        public void Initialize()
        {
            //Data
            _user = DataSupplier.DataSupplier.CreateUsers(_password).First();

            //Mock initialise
            _uow = new Mock<IUnitOfWork>();
            _userRepository = new Mock<IUserRepository>();

            //Mock setup
            _userRepository.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<string>()))
                           .Returns(Task.FromResult(_user));
            _uow.Setup(x => x.GetUserRepository()).Returns(_userRepository.Object);

            //Initialize sut
            _sut = new LoginService(_uow.Object);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task LoginAsyncValidationIsSuccesfulWithCorrectUserNameAndPassword()
        {
            User user = await _sut.Validate(_user.UserName, _password);

            user.UserName.Should().BeEquivalentTo(_user.UserName);
            user.Id.Should().BeEquivalentTo(_user.Id);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [ExpectedException(typeof(ServerValidationException))]
        public async Task LoginAsyncValidationThrowsServerValidationExceptionWithInorrectUserNameAndPassword()
        {
            var wrongUserName = "wrongUserName";
            var wrongPassword = "wrongPassword";
            User nullUser = null;

            _userRepository.Setup(x => x.Validate(wrongUserName, wrongPassword))
                           .Returns(Task.FromResult(nullUser));

            User user = await _sut.Validate(wrongUserName, wrongPassword);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void Dispose()
        {
            _sut.Dispose();

            _uow.Verify(uow => uow.Dispose());
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void DisposeOnce()
        {
            _sut.Dispose();

            _uow.Verify(uow => uow.Dispose(), Times.Once);
        }
    }
}
