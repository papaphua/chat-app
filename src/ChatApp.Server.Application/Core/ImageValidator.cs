using ChatApp.Server.Domain.Core;
using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Application.Core;

public static class ImageValidator
{
    public static bool IsValid(FileExtension extension)
    {
        return ImageExtensions.AvatarExtensionMapping.TryGetValue(extension.ToString().ToLower(), out _);
    }
}