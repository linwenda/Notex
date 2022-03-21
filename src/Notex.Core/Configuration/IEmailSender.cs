namespace Notex.Core.Configuration;

public interface IEmailSender
{
    Task SendAsync(string to, string subject, string content);
}