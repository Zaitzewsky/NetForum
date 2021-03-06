﻿using Domain.Interface;
using Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using UoW;
using UoW.Interface;
using System.Transactions;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;
using System.Linq;
using System;

namespace IntegrationTests.Repository
{
    [TestClass]
    public class UserRepository
    {
        private IUserRepository _sut;
        private IUnitOfWork _unitOfWork;
        private User _user;
        private User _userTwo;
        private string _password;

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWork = new UnitOfWork();
            _sut = _unitOfWork.GetUserRepository();
            _password = "Zaitzewsky12345";
            _user = DataSupplier.DataSupplier.CreateUsers(_password).First();
            _userTwo = DataSupplier.DataSupplier.CreateUsers(_password).LastOrDefault();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _unitOfWork.Dispose();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetUserRoleSuccess()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var expectedUserRole = "ForumUser";
                
                await _sut.Register(_user, _password);
                await _sut.SetForumUserRole(_user);

                var returnedUserRole = _unitOfWork.GetUserRoleById(_user.Id);

                Assert.AreEqual(expectedUserRole, returnedUserRole);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetAllSuccessNotNull()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _sut.Register(_user, _password);
                await _sut.Register(_userTwo, _password);

                var users = await _sut.GetAllAsync();

                Assert.IsNotNull(users);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetAllSuccessContainsMoreThanOne()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _sut.Register(_user, _password);
                await _sut.Register(_userTwo, _password);

                var result = await _sut.GetAllAsync();
                var users = result.ToList();

                Assert.IsTrue(users.Count >= 2);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task RegisterSuccess()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await _sut.Register(_user, _password);
                Assert.IsTrue(result.Succeeded);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task RegisterIsInstanceOfIdentityResult()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await _sut.Register(_user, _password);
                Assert.IsInstanceOfType(result, typeof(IdentityResult));
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(DbEntityValidationException))]
        public async Task RegisterFailOnSameUser()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _sut.Register(_user, _password);
                await _sut.Register(_user, _password);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(DbEntityValidationException))]
        public async Task RegisterFailOnUserNameLongerThanThirtyCharacters()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _user.UserName = string.Concat(Enumerable.Repeat("A", 35));
                await _sut.Register(_user, _password);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetSuccessNotNull()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _sut.Register(_user, _password);
                var user = await _sut.GetAsync(_user);

                Assert.IsNotNull(user);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetSuccessEqualName()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _sut.Register(_user, _password);
                var user = await _sut.GetAsync(_user);

                Assert.AreEqual(user.UserName, _user.UserName);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetFail()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _sut.Register(_user, _password);
                _user.Id = "Wrong ID";
                var user = await _sut.GetAsync(_user);

                Assert.IsNull(user);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ValidateSuccessNotNull()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _sut.Register(_user, _password);
                var user = await _sut.Validate(_user.UserName, _password);

                Assert.IsNotNull(user);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ValidateSuccessEqualUsername()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _sut.Register(_user, _password);
                var user = await _sut.Validate(_user.UserName, _password);

                Assert.AreEqual(_user.UserName, user.UserName);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ValidateFail()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _sut.Register(_user, _password);
                _password = "Wrong password";
                var user = await _sut.Validate(_user.UserName, _password);

                Assert.IsNull(user);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task SetForumUserRoleSuccess()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _sut.Register(_user, _password);
                var identityResult = await _sut.SetForumUserRole(_user);

                Assert.IsTrue(identityResult.Succeeded);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task SetForumUserRoleFailDueToMissingUserId()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                await _sut.SetForumUserRole(_user);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task UpdateAsyncSuccess()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _sut.Register(_user, _password);
                _user.UserName = "UpdatedUsername";
                var identityResult = await _sut.UpdateAsync(_user);

                Assert.IsTrue(identityResult.Succeeded);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task UpdateAsyncIsOfTypeIdentityResult()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _sut.Register(_user, _password);
                _user.UserName = "UpdatedUsername";
                var identityResult = await _sut.UpdateAsync(_user);

                Assert.IsInstanceOfType(identityResult, typeof(IdentityResult));
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task UpdateAsyncSuccessChangesInsertedConfirmation()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _sut.Register(_user, _password);
                _user.UserName = "UpdatedUsername";
                await _sut.UpdateAsync(_user);

                var updatedUser = await _sut.GetAsync(_user);

                Assert.AreEqual(_user.UserName, updatedUser.UserName);
            }
        }
    }
}
