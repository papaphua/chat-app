using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Application.Shared.Dtos;

public sealed class NewResourceDto
{
    public string Name { get; set; } = default!;

    public byte[] Bytes { get; set; } = default!;

    public FileExtension Extension { get; set; } = default!;
}