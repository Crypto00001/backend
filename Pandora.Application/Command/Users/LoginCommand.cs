using System.ComponentModel.DataAnnotations;

namespace Pandora.Application.Command.Users
{
    public class LoginCommand
    {
        [Required]
        [StringLength(200, MinimumLength = 6)]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        
    }
}
