using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Api.Core.Extensions;

public static class FormFileExtensions
{
    private static readonly Dictionary<string, FileExtension> ExtensionMapping = new()
    {
        { "jpg", FileExtension.Jpg },
        { "jpeg", FileExtension.Jpeg },
        { "png", FileExtension.Png },
        { "gif", FileExtension.Gif },
        { "mp4", FileExtension.Mp4 },
        { "mov", FileExtension.Mov },
        { "avi", FileExtension.Avi },
        { "pdf", FileExtension.Pdf },
        { "docx", FileExtension.Docx },
        { "xlsx", FileExtension.Xlsx },
        { "pptx", FileExtension.Pptx },
        { "txt", FileExtension.Txt },
        { "csv", FileExtension.Csv },
        { "zip", FileExtension.Zip },
        { "rar", FileExtension.Rar }
    };

    public static NewResourceDto ToNewResourceDto(this IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        var bytes = memoryStream.ToArray();

        var name = Path.GetFileNameWithoutExtension(file.FileName);
        var extension = Path.GetExtension(file.FileName).TrimStart('.');

        return new NewResourceDto
        {
            Name = name,
            Bytes = bytes,
            Extension = GetFileExtension(extension)
        };
    }

    public static List<NewResourceDto>? ToNewResourceDtoList(this List<IFormFile>? files)
    {
        if (files is null || files.Count == 0) return null;

        return files.Select(file => file.ToNewResourceDto()).ToList();
    }

    private static FileExtension GetFileExtension(string extension)
    {
        if (ExtensionMapping.TryGetValue(extension.ToLower(), out var result)) return result;

        throw new ArgumentException("Unsupported file extension.");
    }
}