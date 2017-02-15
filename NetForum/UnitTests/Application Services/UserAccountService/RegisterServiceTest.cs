using Domain.Interface;
using Domain.Model;
using Exceptions.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using UoW.Interface;
using UserAccountServiceNameSpace.Interface;
using UserAccountServiceNameSpace.Service;

namespace UnitTests.Application_Services.UserAccountService
{
    [TestClass]
    public class RegisterServiceTest
    {
        private Mock<IUnitOfWork> _uow;
        private Mock<IUserRepository> _userRepository;
        private IRegisterService _sut;
        private User _user;
        private string _password;

        #region Setup
        [TestInitialize]
        public void Initialize()
        {
            //Test data
            _password = "HueHue123";
            var users = DataSupplier.DataSupplier.CreateUsers(_password);
            _user = users.First();

            //Mock initialise
            _uow = new Mock<IUnitOfWork>();
            _userRepository = new Mock<IUserRepository>();

            //Mock setup
            _uow.Setup(x => x.GetUserRepository()).Returns(_userRepository.Object);
            _userRepository.Setup(x => x.Register(It.IsAny<User>(), _password)).Returns(Task.FromResult(IdentityResult.Success));

            //System under test initialise
            _sut = new RegisterService(_uow.Object);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _uow.Object.Dispose();
        }
        #endregion

        #region Tests
        [TestMethod]
        [TestCategory("UnitTest")]
        [ExpectedException(typeof(ServerValidationException))]
        public async Task RegisterAsyncSuccessServerValidationSuccess()
        {
            await _sut.Register(_user, _password);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [ExpectedException(typeof(ServerValidationException))]
        public async Task RegisterAsyncSuccessServerValidationError()
        {
            await _sut.Register(_user, _password);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task RegisterAsyncValidationTypeSuccess()
        {
            var exception = new ServerValidationException();

            try
            {
                await _sut.Register(_user, _password);
            }
            catch (ServerValidationException ex)
            {
                exception = ex;
            }

            Assert.AreEqual(ServerValidationException.ServerValidationExceptionType.Success, exception.ValidationExceptionType);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task RegisterAsyncValidationTypeError()
        {
            var exception = new ServerValidationException();

            try
            {
                _userRepository.Setup(x => x.Register(_user, _password)).Returns(Task.FromResult(new IdentityResult()));
                await _sut.Register(_user, _password);
            }
            catch (ServerValidationException ex)
            {
                exception = ex;
            }
            Assert.AreEqual(ServerValidationException.ServerValidationExceptionType.Error, exception.ValidationExceptionType);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task RegisterAsyncCorrectAmountOfRowsInSuccessMessage()
        {
            const int amountOfRows = 1;
            try
            {
                await _sut.Register(_user, _password);
            }
            catch (ServerValidationException ex)
            {
                int numLines = ex.Message.Split('\n').Length;
                Assert.AreEqual(amountOfRows, numLines);
            }
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [ExpectedException(typeof(ServerValidationException))]
        public async Task RegisterDbEntityValidationException()
        {
            _userRepository.Setup(x => x.Register(_user, _password)).ThrowsAsync(new DbEntityValidationException());

            await _sut.Register(_user, _password);
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
        #endregion
    }
}
