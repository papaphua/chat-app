﻿using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Domain.Shared;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Groups;

public sealed class GroupInvitationRepository(ApplicationDbContext dbContext)
    : Repository<GroupInvitation>(dbContext), IGroupInvitationRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<PagedList<GroupInvitation>> GetPagedByGroupIdAndUserIdAsync(Guid groupId, Guid userId,
        InvitationParameters parameters)
    {
        return await _dbContext.Set<GroupInvitation>()
            .Where(invitation => invitation.GroupId == groupId
                                 && invitation.CreatorId == userId)
            .ToPagedListAsync(parameters);
    }

    public async Task<GroupInvitation?> GetByGroupIdAndCreatorIdAsync(Guid groupId, Guid creatorId)
    {
        return await _dbContext.Set<GroupInvitation>()
            .FirstOrDefaultAsync(invitation => invitation.GroupId == groupId
                                               && invitation.CreatorId == creatorId);
    }

    public async Task<GroupInvitation?> GetByGroupIdAndLinkAsync(Guid groupId, string link)
    {
        return await _dbContext.Set<GroupInvitation>()
            .FirstOrDefaultAsync(invitation => invitation.GroupId == groupId
                                               && invitation.Link == link);
    }
}