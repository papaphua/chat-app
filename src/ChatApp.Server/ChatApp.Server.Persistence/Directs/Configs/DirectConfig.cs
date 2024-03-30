using ChatApp.Server.Domain.Directs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Persistence.Directs.Configs;

public sealed class DirectConfig : IEntityTypeConfiguration<Direct>
{
    public void Configure(EntityTypeBuilder<Direct> builder)
    {
        builder.HasMany(direct => direct.Memberships)
            .WithOne(membership => membership.Direct)
            .HasForeignKey(membership => membership.DirectId);

        builder.HasMany(direct => direct.Messages)
            .WithOne(message => message.Direct)
            .HasForeignKey(message => message.DirectId);
    }
}