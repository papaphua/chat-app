using ChatApp.Server.Domain.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Infrastructure.Groups.Configs;

public sealed class GroupConfig : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasMany(group => group.Memberships)
            .WithOne(membership => membership.Group)
            .HasForeignKey(membership => membership.GroupId);

        builder.HasMany(group => group.Messages)
            .WithOne(message => message.Group)
            .HasForeignKey(message => message.GroupId);

        builder.HasMany(group => group.Avatars)
            .WithOne(avatar => avatar.Group)
            .HasForeignKey(message => message.GroupId);

        builder.HasMany(group => group.Roles)
            .WithOne(role => role.Group)
            .HasForeignKey(role => role.GroupId);
        
        builder.HasMany(group => group.Bans)
            .WithOne(ban => ban.Group)
            .HasForeignKey(ban => ban.GroupId);
        
        builder.HasMany(group => group.Requests)
            .WithOne(request => request.Group)
            .HasForeignKey(request => request.GroupId);
    }
}