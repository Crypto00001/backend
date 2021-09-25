using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pandora.Domain.Domain;

namespace Pandora.Infrastructure.Config
{
    public class PlanConfig : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(c => c.MinimumDeposit).IsRequired();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.ProfitPercent).IsRequired();

            builder.ToTable("Plan");
        }
    }
}
