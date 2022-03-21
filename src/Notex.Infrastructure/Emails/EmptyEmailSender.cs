using Notex.Core.Configuration;
using Notex.Core.Lifetimes;

namespace Notex.Infrastructure.Emails;

public class EmptyEmailSender : IEmailSender, IScopedLifetime
{
    public Task SendAsync(string to, string subject, string content)
    {
        return Task.CompletedTask;
    }
}