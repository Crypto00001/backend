using System.ComponentModel.DataAnnotations;

namespace Pandora.Application.Command.Users
{
    public class UpdatePasswordUserCommand
    {
        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

    }
}
