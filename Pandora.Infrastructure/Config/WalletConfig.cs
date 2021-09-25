using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pandora.Domain.Domain;

namespace Pandora.Infrastructure.Config
{
    public class WalletConfig : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.Property(c => c.Balance).IsRequired();
            builder.Property(c => c.InvestedBalance).IsRequired();
            builder.Property(c => c.WalletAddress).IsRequired();
            builder.Property(c => c.WalletType).IsRequired();
            builder.Property(c => c.AvailableBalance).IsRequired();

            builder.ToTable("Wallet");
        }
    }
}
