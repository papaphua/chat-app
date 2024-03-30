using ChatApp.Server.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Infrastructure.Users.Configs;

public sealed class UserAvatarConfig : IEntityTypeConfiguration<UserAvatar>
{
    public void Configure(EntityTypeBuilder<UserAvatar> builder)
    {
        builder.HasKey(avatar => new { avatar.UserId, avatar.ResourceId });

        builder.HasOne(avatar => avatar.Resource)
            .WithOne()
            .HasForeignKey<UserAvatar>(avatar => avatar.ResourceId);
    }
}