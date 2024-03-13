﻿using ChatApp.Server.Domain.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Infrastructure.Groups.Configs;

public sealed class GroupMembershipConfig : IEntityTypeConfiguration<GroupMembership>
{
    public void Configure(EntityTypeBuilder<GroupMembership> builder)
    {
        builder.HasKey(membership => new { membership.ChatId, membership.MemberId });

        builder.HasOne(membership => membership.Role)
            .WithMany()
            .HasForeignKey(membership => membership.RoleId);
    }
}