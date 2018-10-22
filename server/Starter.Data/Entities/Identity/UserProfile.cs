

namespace Starter.Data.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public UserAccount Identity { get; set; }
    }
}
