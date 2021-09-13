namespace MarchNote.Infrastructure.Attachments
{
    public class LocalAttachmentSetting
    {
        public string SavePath { get; }

        public LocalAttachmentSetting(string savePath)
        {
            SavePath = savePath;
        }
    }
}