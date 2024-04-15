using ChatApp.Server.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Persistence.Users.Configs;

public sealed class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasMany(user => user.Avatars)
            .WithOne(avatar => avatar.User)
            .HasForeignKey(avatar => avatar.UserId);

        builder.HasMany(user => user.Contacts)
            .WithOne(contact => contact.Owner)
            .HasForeignKey(contact => contact.OwnerId);

        builder.HasMany(user => user.DirectMemberships)
            .WithOne(membership => membership.Member)
            .HasForeignKey(membership => membership.MemberId);

        builder.HasMany(user => user.GroupMemberships)
            .WithOne(membership => membership.Member)
            .HasForeignKey(membership => membership.MemberId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}