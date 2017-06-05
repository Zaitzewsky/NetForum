using System.Threading.Tasks;
using Domain.Model;
using Viewmodels.UserAccount;

namespace UserAccountFacade.Interface
{
    public interface ILoginFacade
    {
        Task<UserViewmodel> Validate(string userName, string password);
        UserViewmodel MapUserViewModelFromUser(User user);
    }
}
