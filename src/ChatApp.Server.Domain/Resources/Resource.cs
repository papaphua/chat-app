using System.ComponentModel.DataAnnotations;
using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Resources;

public sealed class Resource(string name, byte[] bytes, FileExtension extension) : IEntity<Guid>
{
    [MaxLength(64)]
    public string Name { get; set; } = name;

    public byte[] Bytes { get; set; } = bytes;

    public FileExtension Extension { get; set; } = extension;
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public Guid Id { get; set; } = Guid.NewGuid();
}