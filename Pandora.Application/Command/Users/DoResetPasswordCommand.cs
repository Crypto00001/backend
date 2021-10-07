using System.ComponentModel.DataAnnotations;

namespace Pandora.Application.Command.Users
{
    public class DoResetPasswordCommand
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Must be between 6 and 50 characters", MinimumLength = 6)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "ResetCode is required")]
        public string ResetCode { get; set; }

        [Required(ErrorMessage = "NewPassword is required")]
        [StringLength(30, ErrorMessage = "Must be between 6 and 30 characters", MinimumLength = 6)]
        public string NewPassword { get; set; }
    }
}
