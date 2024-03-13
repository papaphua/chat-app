using ChatApp.Server.Domain.Directs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Infrastructure.Directs.Configs;

public sealed class DirectMembershipConfig : IEntityTypeConfiguration<DirectMembership>
{
    public void Configure(EntityTypeBuilder<DirectMembership> builder)
    {
        builder.HasKey(membership => new { membership.ChatId, membership.MemberId });
    }
}