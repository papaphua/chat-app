using ChatApp.Server.Domain.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Infrastructure.Groups.Configs;

public sealed class GroupRoleConfig : IEntityTypeConfiguration<GroupRole>
{
    public void Configure(EntityTypeBuilder<GroupRole> builder)
    {
        builder.HasOne(role => role.Group)
            .WithMany()
            .HasForeignKey(role => role.GroupId);
    }
}