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
            builder.Property(c => c.Amount).IsRequired();
            builder.Property(c => c.WithdrawalNumber).IsRequired();
            builder.Property(c => c.WalletAddress).IsRequired();
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.WalletType).IsRequired();

            builder.ToTable("Withdrawal");
        }
    }
}
