namespace AbuInt.Service.Helpers;

public class EnvironmentHelper
{
    public static string WebRootPath { get; set; }
    public static string AttachmentPath => Path.Combine(WebRootPath, "images");
    public static string Attachment => "images";

    public static string FilePath => Path.Combine(WebRootPath, "file");
    public static string File => "file";
}
