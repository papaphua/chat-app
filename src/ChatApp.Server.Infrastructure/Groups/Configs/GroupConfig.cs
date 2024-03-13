using ChatApp.Server.Domain.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Infrastructure.Groups.Configs;

public sealed class GroupConfig : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasMany(group => group.Memberships)
            .WithOne(membership => membership.Chat)
            .HasForeignKey(membership => membership.ChatId);

        builder.HasMany(group => group.Messages)
            .WithOne(message => message.Chat)
            .HasForeignKey(message => message.ChatId);
        
        builder.HasMany(group => group.Avatars)
            .WithOne(avatar => avatar.Chat)
            .HasForeignKey(message => message.ChatId);
        
        builder.HasMany(group => group.Roles)
            .WithOne(role => role.Chat)
            .HasForeignKey(role => role.ChatId);
    }
}