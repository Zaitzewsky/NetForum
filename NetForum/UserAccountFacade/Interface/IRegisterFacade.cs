using Domain.Model;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Viewmodels.UserAccount;

namespace UserAccountFacade.Interface
{
    public interface IRegisterFacade
    {
        Task<IdentityResult> Register(UserViewmodel user, string password);
        User MapUserFromUserViewModel(UserViewmodel userViewmodel);
        void Dispose();
    }
}
