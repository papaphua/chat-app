using ChatApp.Server.Domain.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Persistence.Groups.Configs;

public sealed class GroupBanConfig : IEntityTypeConfiguration<GroupBan>
{
    public void Configure(EntityTypeBuilder<GroupBan> builder)
    {
        builder.HasKey(ban => new { ban.GroupId, ban.UserId });

        builder.HasOne(ban => ban.User)
            .WithMany()
            .HasForeignKey(ban => ban.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}