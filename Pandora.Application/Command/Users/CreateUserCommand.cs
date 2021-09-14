using Pandora.Domain.Domain;
using System.ComponentModel.DataAnnotations;

namespace Pandora.Application.Command.Users
{
    public class CreateUserCommand
    {
        [Required]
        [StringLength(200, MinimumLength = 10)]
        public string UserName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string PasswordHash { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 20)]
        public string Email { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 10)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Country { get; set; }
        public User ToUser() => new User
        {
            UserName = UserName,
            PasswordHash = PasswordHash,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            Country = Country
        };

    }
}
