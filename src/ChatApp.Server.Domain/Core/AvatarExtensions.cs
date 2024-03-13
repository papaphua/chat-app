using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Domain.Core;

public static class AvatarExtensions
{
    private static readonly Dictionary<string, FileExtension> AvatarExtensionMapping = new()
    {
        { "jpg", FileExtension.Jpg },
        { "jpeg", FileExtension.Jpeg },
        { "png", FileExtension.Png },
        { "gif", FileExtension.Gif }
    };
}