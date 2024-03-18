using System.ComponentModel.DataAnnotations;
using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Resources;

public sealed class Resource : IEntity<Guid>
{
    [MaxLength(64)] public string Name { get; set; } = default!;

    public byte[] Bytes { get; set; } = default!;

    public FileExtension Extension { get; set; }

    public Guid Id { get; set; } = Guid.NewGuid();
}