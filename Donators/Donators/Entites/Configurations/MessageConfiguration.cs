using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Donators.Entites.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id)
            .ValueGeneratedOnAdd();
        builder.Property(_ => _.Content)
            .IsRequired();
    }
}
