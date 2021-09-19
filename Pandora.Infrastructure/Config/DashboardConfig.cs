using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pandora.Domain.Domain;

namespace Pandora.Infrastructure.Config
{
    public class DashboardConfig : IEntityTypeConfiguration<Dashboard>
    {
        public void Configure(EntityTypeBuilder<Dashboard> builder)
        {
            builder.Property(c => c.ItemName).IsRequired();
            builder.Property(c => c.ItemValue).IsRequired();

            builder.ToTable("Dashboard");
        }
    }
}
