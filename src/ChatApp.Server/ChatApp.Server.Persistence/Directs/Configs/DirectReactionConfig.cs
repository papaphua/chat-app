using ChatApp.Server.Domain.Directs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Infrastructure.Directs.Configs;

public sealed class DirectReactionConfig : IEntityTypeConfiguration<DirectReaction>
{
    public void Configure(EntityTypeBuilder<DirectReaction> builder)
    {
        builder.HasOne(reaction => reaction.User)
            .WithMany()
            .HasForeignKey(reaction => reaction.UserId);
    }
}