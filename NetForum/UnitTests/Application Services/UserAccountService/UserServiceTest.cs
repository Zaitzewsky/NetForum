﻿using Domain.Interface;
using Domain.Model;
using Exceptions.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UoW.Interface;
using UserAccountServiceNameSpace.Interface;
using UserAccountServiceNameSpace.Service;

namespace UnitTests.Application_Services.UserAccountService
{
    [TestClass]
    public class UserServiceTest
    {
        private IUserService _sut;
        private Mock<IUnitOfWork> _uow;
        private Mock<IUserRepository> _userRepository;
        private User _user;

        #region Setup
        [TestInitialize]
        public void Initialise()
        {
            //Test data
            var password = "HueHue123";
            var users = DataSupplier.DataSupplier.CreateUsers(password);
            var asyncUsers = Task.FromResult(users);
            var asyncUser = Task.FromResult(users.First());
            _user = users.First();

            //Mock initialise
            _uow = new Mock<IUnitOfWork>();
            _userRepository = new Mock<IUserRepository>();

            //Mock setup
            _userRepository.Setup(x => x.GetAllAsync()).Returns(asyncUsers);
            _userRepository.Setup(x => x.GetAsync(It.IsAny<User>())).Returns(asyncUser);
            _userRepository.Setup(x => x.UpdateAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
            _uow.Setup(x => x.GetUserRepository()).Returns(_userRepository.Object);

            //System under test initialise
            _sut = new UserService(_uow.Object);
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
        public async Task GetAllAsyncNotNull()
        {
            var users = await _sut.GetAllAsync();
            Assert.IsNotNull(users);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [ExpectedException(typeof(ServerValidationException))]
        public async Task GetAllAsyncNoUsersFoundExceptionThrown()
        {
            IEnumerable<User> emptyListOfUSers = new List<User>();
            var usersAsync = Task.FromResult(emptyListOfUSers);

            _userRepository.Setup(x => x.GetAllAsync()).Returns(usersAsync);

            await _sut.GetAllAsync();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [ExpectedException(typeof(ServerValidationException))]
        public async Task GetAllAsyncNullExceptionThrown()
        {
            var usersAsync = Task.FromResult<IEnumerable<User>>(null);
            _userRepository.Setup(x => x.GetAllAsync()).Returns(usersAsync);

            await _sut.GetAllAsync();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [ExpectedException(typeof(ServerValidationException))]
        public async Task GetAsyncNoUserFoundExceptionThrown()
        {
            var nullUser = Task.FromResult<User>(null);
            _userRepository.Setup(x => x.GetAsync(It.IsAny<User>())).Returns(nullUser);

            await _sut.GetAsync(new User());
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task GetAsync()
        {
            var user = await _sut.GetAsync(_user);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task UpdateAsyncIdentityResultSuccess()
        {
            var identityResult = await _sut.UpdateAsync(_user);

            Assert.IsTrue(identityResult.Succeeded);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task UpdateAsyncIdentityResultError()
        {
            _userRepository.Setup(x => x.UpdateAsync(It.IsAny<User>())).Returns(Task.FromResult(new IdentityResult()));
            var identityResult = await _sut.UpdateAsync(_user);

            Assert.IsFalse(identityResult.Succeeded);
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

            _uow.Verify(uow => uow.Dispose(), Times.AtMostOnce);
        }
        #endregion
    }
}
