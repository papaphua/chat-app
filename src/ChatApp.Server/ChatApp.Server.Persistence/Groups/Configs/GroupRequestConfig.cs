using ChatApp.Server.Domain.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Persistence.Groups.Configs;

public sealed class GroupRequestConfig : IEntityTypeConfiguration<GroupRequest>
{
    public void Configure(EntityTypeBuilder<GroupRequest> builder)
    {
        builder.HasKey(request => new { request.GroupId, request.UserId });

        builder.HasOne(request => request.User)
            .WithMany()
            .HasForeignKey(request => request.UserId);
    }
}