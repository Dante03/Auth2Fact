using System.ComponentModel.DataAnnotations;

namespace WebApplication9.Models
{
    public class UserLoginRequest
    {
        public UserLoginRequest()
        {
        }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? email { get; set; }

        [Required]
        public string? password { get; set; }
    }
}
