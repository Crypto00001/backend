using System.ComponentModel.DataAnnotations;

namespace Pandora.Application.Command.Users
{
    public class UpdatePasswordUserCommand
    {
        [Required(ErrorMessage = "OldPassword is required")]
        [StringLength(30, ErrorMessage = "Must be between 6 and 30 characters", MinimumLength = 6)]
        public string OldPassword { get; set; }
        
        [Required(ErrorMessage = "NewPassword is required")]
        [StringLength(30, ErrorMessage = "Must be between 6 and 30 characters", MinimumLength = 6)]
        public string NewPassword { get; set; }

    }
}
