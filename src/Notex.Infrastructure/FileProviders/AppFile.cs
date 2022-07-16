namespace Notex.Infrastructure.FileProviders;

public class AppFile
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public DateTime CreationTime { get; set; }
    public string Name { get; set; }
    public byte[] Content { get; set; }
    public string ContentType { get; set; }
}