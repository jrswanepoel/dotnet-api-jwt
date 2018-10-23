using Microsoft.AspNetCore.Identity;

namespace Starter.Data.Entities
{
    public class UserAccount : IdentityUser
    {
        public UserProfile Profile { get; set; }
    }
}
