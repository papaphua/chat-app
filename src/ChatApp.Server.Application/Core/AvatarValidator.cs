using ChatApp.Server.Domain.Core;
using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Application.Core;

public static class AvatarValidator
{
    public static bool IsValid(FileExtension extension)
    {
        return AvatarFileExtensions.AvatarExtensionMapping.TryGetValue(extension.ToString().ToLower(), out _);
    }
}