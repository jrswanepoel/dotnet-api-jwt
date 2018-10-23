using Microsoft.AspNetCore.Identity;

namespace Starter.Data.Entities
{
    public class UserProfile
    {
        [PersonalData]
        public int Id { get; set; }
        [PersonalData]
        public string IdentityId { get; set; }
        [PersonalData]
        public UserAccount Identity { get; set; }

        [ProtectedPersonalData]
        public string FullName { get; set; }
        [ProtectedPersonalData]
        public string NickName { get; set; }
    }
}
