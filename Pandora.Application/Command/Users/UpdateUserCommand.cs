using System.ComponentModel.DataAnnotations;

namespace Pandora.Application.Command.Users
{
    public class UpdateUserCommand
    {
        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Email { get; set; }
        [StringLength(250)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(150)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string Country { get; set; }

    }    
}
