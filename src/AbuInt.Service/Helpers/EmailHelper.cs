using AbuInt.Service.DTOs.Users;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace AbuInt.Service.Helpers;

public class EmailHelper
{
    private readonly IConfigurationSection config;

    public EmailHelper(IConfiguration configuration)
    {
        config = configuration.GetSection("Email");
    }

    public async Task SendAsync(EmailMessage emailMesage)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(config["EmailAddress"]));
        email.To.Add(MailboxAddress.Parse(emailMesage.To));
        email.Subject = emailMesage.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = emailMesage.Body.ToString() };

        var smtp = new SmtpClient();
        await smtp.ConnectAsync(config["Host"], 587, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(config["EmailAddress"], config["Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
