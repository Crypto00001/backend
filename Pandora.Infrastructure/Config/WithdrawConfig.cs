using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pandora.Domain.Domain;
using System.Data;

namespace Pandora.Infrastructure.Config
{
    public class WithdrawalConfig : IEntityTypeConfiguration<Withdrawal>
    {
        public void Configure(EntityTypeBuilder<Withdrawal> builder)
        {
            builder.Property(c => c.Amount).HasColumnType(nameof(SqlDbType.Decimal)).IsRequired();
            builder.Property(c => c.WithdrawNumber).IsRequired();
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.WalletType).HasColumnType(nameof(SqlDbType.Decimal)).IsRequired();

            builder.ToTable("Withdrawal");
        }
    }
}
