using Microsoft.AspNetCore.Identity;

namespace WebApplication9.Entities
{
    public class User : IdentityUser
    {
        public User() { }


        public int AccountId { get; set; }
        public Guid AccountGuid { get; set; }
        public string Salt { get; set; }
        public bool Verified { get; set; }
        public string Checksum { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
