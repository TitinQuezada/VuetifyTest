using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations
{
    public sealed class SystemUsersConfiguration : IEntityTypeConfiguration<SystemUser>
    {
        void IEntityTypeConfiguration<SystemUser>.Configure(EntityTypeBuilder<SystemUser> builder)
        {
            builder.Property(systemUser => systemUser.Name).HasMaxLength(30).IsRequired();
            builder.Property(systemUser => systemUser.Lastname).HasMaxLength(30).IsRequired();
            builder.Property(systemUser => systemUser.Password).IsRequired();
            builder.Property(systemUser => systemUser.Username).HasMaxLength(20).IsRequired();
            builder.HasIndex(systemUser => systemUser.Username).IsUnique();
            builder.Property(systemUser => systemUser.Email).HasMaxLength(60).IsRequired();
            builder.HasIndex(systemUser => systemUser.Email).IsUnique();
            builder.Property(systemUser => systemUser.CreatedDate).IsRequired();
        }
    }
}
