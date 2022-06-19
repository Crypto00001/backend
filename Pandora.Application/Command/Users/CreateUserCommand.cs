using Pandora.Domain.Domain;
using System.ComponentModel.DataAnnotations;

namespace Pandora.Application.Command.Users
{
    public class CreateUserCommand
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Must be between 6 and 50 characters", MinimumLength = 6)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, ErrorMessage = "Must be between 6 and 30 characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(50, ErrorMessage = "Must be maximum 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(50, ErrorMessage = "Must be maximum 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(50, ErrorMessage = "Must be maximum 50 characters")]
        public string Country { get; set; }
        
        [StringLength(6, ErrorMessage = "Must be maximum 6 characters")]
        public string ReferralCode { get; set; }
        public User ToUser() => new User
        {
            PasswordHash = Password,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            Country = Country,
            ReferralCode = ReferralCode
        };

    }    
}
