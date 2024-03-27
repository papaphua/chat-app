using System.ComponentModel.DataAnnotations;

namespace ChatApp.Server.Domain.Resources;

public sealed class Resource
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(64)] public string Name { get; set; } = default!;

    public byte[] Bytes { get; set; } = default!;

    public FileExtension Extension { get; set; }
}