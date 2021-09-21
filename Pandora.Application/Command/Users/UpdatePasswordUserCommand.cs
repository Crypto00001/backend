using System.ComponentModel.DataAnnotations;

namespace Pandora.Application.Command.Users
{
    public class UpdatePasswordUserCommand
    {
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string OldPassword { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string NewPassword { get; set; }

    }
}
