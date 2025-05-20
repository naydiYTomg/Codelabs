using MimeKit;
using MailKit.Net.Smtp;
using Codelabs.Core;

namespace Codelabs.BLL
{
    public static class MailService
    {
        public static async Task SendMessage(string admin_name, string to, string text)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация CODE/\\ABS", Options.GmailAddress));
            emailMessage.To.Add(new MailboxAddress("", to));
            emailMessage.Subject = admin_name;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = text
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 25, false);
                await client.AuthenticateAsync(Options.GmailAddress, Options.GmailPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
