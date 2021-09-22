using Pandora.Domain.Domain;
using System.ComponentModel.DataAnnotations;

namespace Pandora.Application.Command.Referrals
{
    public class CreateReferralCommand
    {
        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Email { get; set; }

    }
}
