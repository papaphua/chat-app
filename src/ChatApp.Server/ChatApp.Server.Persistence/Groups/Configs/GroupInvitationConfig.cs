using ChatApp.Server.Domain.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Persistence.Groups.Configs;

public sealed class GroupInvitationConfig : IEntityTypeConfiguration<GroupInvitation>
{
    public void Configure(EntityTypeBuilder<GroupInvitation> builder)
    {
        builder.HasKey(invitation => new { invitation.GroupId, invitation.CreatorId });

        builder.HasOne(invitation => invitation.Creator)
            .WithMany()
            .HasForeignKey(invitation => invitation.CreatorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}