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
            builder.Property(c => c.Balance).HasColumnType(nameof(SqlDbType.Decimal)).IsRequired();
            builder.Property(c => c.InvestedBalance).HasColumnType(nameof(SqlDbType.Decimal)).IsRequired();
            builder.Property(c => c.Address).IsRequired();
            builder.Property(c => c.Type).IsRequired();
            builder.Property(c => c.AvailableBalance).HasColumnType(nameof(SqlDbType.Decimal)).IsRequired();

            builder.ToTable("Wallet");
        }
    }
}
