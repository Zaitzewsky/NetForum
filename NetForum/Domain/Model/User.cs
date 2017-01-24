using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Model
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
