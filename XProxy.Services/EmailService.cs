using Microsoft.AspNetCore.Identity.UI.Services;

namespace XProxy.Services;

public class EmailService : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        //TODO implement telegram notification for confirmation
        return Task.CompletedTask;
    }
}