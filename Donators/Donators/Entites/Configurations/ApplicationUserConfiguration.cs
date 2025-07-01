namespace Donators.Entites.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
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
        builder.OwnsMany(x => x.RefreshTokens)
                .ToTable("RefreshTokens")
                .WithOwner()
                .HasForeignKey("UserId");
    }
}
