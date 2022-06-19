using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pandora.Domain.Domain;

namespace Pandora.Infrastructure.Config
{
    public class ReferralConfig : IEntityTypeConfiguration<Referral>
    {
        public void Configure(EntityTypeBuilder<Referral> builder)
        {
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.InvitedUserId).IsRequired();

            builder.ToTable("Referral");
        }
    }
}
