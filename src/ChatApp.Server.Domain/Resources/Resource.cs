using ChatApp.Server.Domain.Core;

namespace ChatApp.Server.Domain.Resources;

public sealed class Resource(string name, byte[] bytes, FileExtension extension)
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = name;

    public byte[] Bytes { get; set; } = bytes;

    public FileExtension Extension { get; set; } = extension;
}