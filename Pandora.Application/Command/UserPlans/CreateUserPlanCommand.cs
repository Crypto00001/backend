using Pandora.Application.Enums;
using Pandora.Domain.Domain;
using System.ComponentModel.DataAnnotations;

namespace Pandora.Application.Command.UserPlans
{
    public class CreateUserPlanCommand
    {
        [Required]
        public string PlanName { get; set; }
        [Required]
        public WalletType WalletType { get; set; }
        [Required]
        public double InvestmentAmount { get; set; }

    }
}
