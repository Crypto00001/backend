using System.ComponentModel.DataAnnotations;

namespace Pandora.Application.Command.Users
{
    public class UpdateUserCommand
    {
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Email { get; set; }
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(30)]
        public string Country { get; set; }

    }    
}
