using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Donators.Entites.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(_ => _.LastName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(_ => _.CharityName)
            .IsRequired()
            .HasMaxLength(100);
    }
}
