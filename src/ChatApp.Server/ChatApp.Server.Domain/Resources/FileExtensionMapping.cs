namespace ChatApp.Server.Domain.Resources;

public static class FileExtensionMapping
{
    public static readonly Dictionary<string, FileExtension> All = new()
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

    public static readonly Dictionary<string, FileExtension> Images = new()
    {
        { "jpg", FileExtension.Jpg },
        { "jpeg", FileExtension.Jpeg },
        { "png", FileExtension.Png },
        { "gif", FileExtension.Gif }
    };

    public static bool IsImage(Resource resource)
    {
        return Images.TryGetValue(resource.Extension.ToString().ToLower(), out _);
    }

    public static FileExtension GetFileExtension(string extension)
    {
        if (All.TryGetValue(extension.ToLower(), out var result)) return result;

        throw new ArgumentException("Unsupported file extension.");
    }
}