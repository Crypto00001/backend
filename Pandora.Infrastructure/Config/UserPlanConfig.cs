using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pandora.Domain.Domain;

namespace Pandora.Infrastructure.Config
{
    public class UserPlanConfig : IEntityTypeConfiguration<UserPlan>
    {
        public void Configure(EntityTypeBuilder<UserPlan> builder)
        {
            builder.Property(c => c.AccruedProfit).IsRequired();
            builder.Property(c => c.InvestmentAmount).IsRequired();
            builder.Property(c => c.WalletType).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.PlanId).IsRequired();
            builder.Property(c => c.UserId).IsRequired();

            builder.ToTable("UserPlan");
        }
    }
}
