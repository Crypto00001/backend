using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pandora.Domain.Domain;
using System.Data;

namespace Pandora.Infrastructure.Config
{
    public class WalletConfig : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.Property(c => c.Balance).IsRequired();
            builder.Property(c => c.InvestedBalance).IsRequired();
            builder.Property(c => c.Address).IsRequired();
            builder.Property(c => c.Type).IsRequired();
            builder.Property(c => c.AvailableBalance).IsRequired();

            builder.ToTable("Wallet");
        }
    }
}
