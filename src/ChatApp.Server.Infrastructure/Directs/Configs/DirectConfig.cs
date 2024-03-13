using ChatApp.Server.Domain.Directs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Infrastructure.Directs.Configs;

public sealed class DirectConfig : IEntityTypeConfiguration<Direct>
{
    public void Configure(EntityTypeBuilder<Direct> builder)
    {
        builder.HasMany(direct => direct.Memberships)
            .WithOne(membership => membership.Chat)
            .HasForeignKey(membership => membership.ChatId);

        builder.HasMany(direct => direct.Messages)
            .WithOne(message => message.Chat)
            .HasForeignKey(message => message.ChatId);
    }
}