using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pandora.Domain.Domain;
using System.Data;

namespace Pandora.Infrastructure.Config
{
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(c => c.Amount).HasColumnType(nameof(SqlDbType.Decimal)).IsRequired();
            builder.Property(c => c.PaymentNumber).IsRequired();
            builder.Property(c => c.TransactionId).IsRequired();
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.WalletType).HasColumnType(nameof(SqlDbType.Decimal)).IsRequired();

            builder.ToTable("Payment");
        }
    }
}
