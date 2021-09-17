using Pandora.Domain.Domain;
using System.ComponentModel.DataAnnotations;

namespace Pandora.Application.Command.Users
{
    public class CreateUserCommand
    {
        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [StringLength(250)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(150)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string Country { get; set; }
        public User ToUser() => new User
        {
            PasswordHash = Password,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            Country = Country
        };

    }
}
