using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Donators.Entites.Configurations;

public class DonatorConfiguration : IEntityTypeConfiguration<Donator>
{
    public void Configure(EntityTypeBuilder<Donator> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id)
            .ValueGeneratedOnAdd();
        builder.Property(_ => _.Name)
            .IsRequired()
            .HasMaxLength(150);
        builder.Property(_ => _.PhoneNumber)
            .IsRequired()
            .HasMaxLength(14);
    }
}
