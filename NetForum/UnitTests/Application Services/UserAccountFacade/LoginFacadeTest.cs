using Domain.Model;
using Mapping.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using UserAccountFacade.Facade;
using UserAccountFacade.Interface;
using UserAccountServiceNameSpace.Interface;
using Viewmodels.UserAccount;
using FluentAssertions;

namespace UnitTests.Application_Services.UserAccountFacade
{
    [TestClass]
    public class LoginFacadeTest
    {
        private ILoginFacade _sut;
        private UserViewmodel _userViewModel;
        private User _user;
        private string _password;

        #region Initialize
        [TestInitialize]
        public void Initialize()
        {
            //Test data
            _password = "Password";
            var userViewmodels = DataSupplier.DataSupplier.CreateUserViewmodels(_password);
            _userViewModel = userViewmodels.First();

            _user = DataSupplier.DataSupplier.CreateUsers(_password).First();

            //Mock initialise
            var loginServiceMock = new Mock<ILoginService>();

            //Configure automapper
            var mapperConfig = new AutoMapperConfiguration();
            var mapper = mapperConfig.Map();

            //Mock setup
            loginServiceMock.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(_user));

            //System under test initialise
            _sut = new LoginFacade(loginServiceMock.Object, mapper);
        }
        #endregion

        #region Tests
        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task LoginAsyncValidationTypeSuccess()
        {
            UserViewmodel userViewmodel = await _sut.Validate(_userViewModel.UserName, _password);

            userViewmodel.Should().BeOfType<UserViewmodel>();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void MapFromUserViewmodelToUserCorrectType()
        {
            UserViewmodel userViewmodel = _sut.MapUserViewModelFromUser(_user);

            userViewmodel.Should().BeOfType<UserViewmodel>();
        }
        #endregion
    }
}
