using Domain.Model;
using Mapping.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using UserAccountFacade.Facade;
using UserAccountFacade.Interface;
using UserAccountServiceNameSpace.Interface;
using Viewmodels.UserAccount;

namespace UnitTests.Application_Services.UserAccountFacade
{
    [TestClass]
    public class RegisterFacadeTest
    {
        private IRegisterFacade _sut;
        private UserViewmodel _user;
        private string _password;

        #region Initialize
        [TestInitialize]
        public void Initialize()
        {
            //Test data
            _password = "HueHue123";
            var users = DataSupplier.DataSupplier.CreateUserViewmodels(_password);
            _user = users.First();

            //Mock initialise
            var registerServiceMock = new Mock<IRegisterService>();

            //Configure automapper
            var mapperConfig = new AutoMapperConfiguration();
            var mapper = mapperConfig.Map();

            //Mock setup
            registerServiceMock.Setup(x => x.Register(It.IsAny<User>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));

            //System under test initialise
            _sut = new RegisterFacade(registerServiceMock.Object, mapper);
        }
        #endregion

        #region Tests
        [TestMethod]
        [TestCategory("UnitTest")]
        public async Task RegisterAsyncValidationTypeSuccess()
        {
            var identityResult = await _sut.Register(_user, _password);

            Assert.IsTrue(identityResult.Succeeded);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void MapFromUserViewmodelToUserCorrectType()
        {
            var user = _sut.MapUserFromUserViewModel(_user);

            Assert.IsInstanceOfType(user, typeof(User));
        } 
        #endregion
    }
}
