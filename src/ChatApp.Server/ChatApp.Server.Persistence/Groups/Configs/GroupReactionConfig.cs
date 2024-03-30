using ChatApp.Server.Domain.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Persistence.Groups.Configs;

public sealed class GroupReactionConfig : IEntityTypeConfiguration<GroupReaction>
{
    public void Configure(EntityTypeBuilder<GroupReaction> builder)
    {
        builder.HasOne(reaction => reaction.User)
            .WithMany()
            .HasForeignKey(reaction => reaction.UserId);
    }
}