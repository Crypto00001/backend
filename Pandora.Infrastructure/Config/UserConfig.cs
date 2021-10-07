using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pandora.Domain.Domain;

namespace Pandora.Infrastructure.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(c => c.Email).IsRequired();
            builder.Property(c => c.PasswordHash).IsRequired();
            builder.Property(c => c.FirstName).IsRequired();
            builder.Property(c => c.LastName).IsRequired();
            builder.Property(c => c.Country).IsRequired();
            builder.Property(c => c.LastLoginAttemptAt);
            builder.Property(c => c.LoginFailedAttemptsCount);
            builder.Property(c => c.ReferralCode);
            builder.Property(c => c.ResetPasswordCode);

            builder.ToTable("User");
        }
    }
}
