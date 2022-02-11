namespace SmartNote.Core.Services;

public interface IEmailSender
{
    Task SendAsync(string email);
}