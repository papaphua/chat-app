﻿using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Domain.Directs;

public sealed class DirectAttachment(Guid messageId, Guid resourceId)
{
    public Guid MessageId { get; set; } = messageId;

    public DirectMessage Message { get; set; } = default!;

    public Guid ResourceId { get; set; } = resourceId;

    public Resource Resource { get; set; } = default!;
}