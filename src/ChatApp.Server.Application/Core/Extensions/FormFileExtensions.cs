using ChatApp.Server.Domain.Resources;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Server.Application.Core.Extensions;

public static class FormFileExtensions
{
    public static Resource ToResource(this IFormFile file)
    {
        using var stream = new MemoryStream();
        file.CopyTo(stream);

        return new Resource
        {
            Name = Path.GetFileNameWithoutExtension(file.FileName),
            Bytes = stream.ToArray(),
            Extension = FileExtensionMapping.GetFileExtension(Path.GetExtension(file.FileName).TrimStart('.'))
        };
    }

    public static List<Resource> ToResources(this List<IFormFile>? files)
    {
        if (files is null || files.Count == 0) return [];

        return files.Select(file => file.ToResource())
            .ToList();
    }
}