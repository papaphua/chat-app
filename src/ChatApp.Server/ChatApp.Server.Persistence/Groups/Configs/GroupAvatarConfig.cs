﻿using ChatApp.Server.Domain.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Persistence.Groups.Configs;

public sealed class GroupAvatarConfig : IEntityTypeConfiguration<GroupAvatar>
{
    public void Configure(EntityTypeBuilder<GroupAvatar> builder)
    {
        builder.HasKey(avatar => new { avatar.GroupId, avatar.ResourceId });

        builder.HasOne(avatar => avatar.Resource)
            .WithOne()
            .HasForeignKey<GroupAvatar>(avatar => avatar.ResourceId);
    }
}