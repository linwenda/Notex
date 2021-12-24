namespace SmartNote.Application.Configuration.Files;

public class UploadResult
{
    private UploadResult(
        bool succeeded,
        string message,
        string savePath,
        string fileName)
    {
        Succeeded = succeeded;
        Message = message;
        SavePath = savePath;
        FileName = fileName;
    }

    public bool Succeeded { get; }
    public string Message { get; }
    public string SavePath { get; }
    public string FileName { get; }

    public static UploadResult Success(string savePath, string fileName)
    {
        return new UploadResult(true, "", savePath, fileName);
    }

    public static UploadResult Failure(string message)
    {
        return new UploadResult(false, message, "", "");
    }
}