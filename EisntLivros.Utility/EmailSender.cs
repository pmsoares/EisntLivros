using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EisntLivros.Utility
{
    public class EmailSender : IEmailSender
    {
        public string SendGridSecret { get; set; }

        public EmailSender(IConfiguration _config)
        {
            SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey")!;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //SEND BY GMAIL SMTP
            MimeMessage emailToSend = new();
            emailToSend.From.Add(MailboxAddress.Parse("admin@site.com"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            using (SmtpClient emailClient = new())
            {
                emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("admin@site.com", "PASSWORD");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }
            return Task.CompletedTask;


            //SEND BY SENDGRID
            //SendGridClient client = new(SendGridSecret);
            //EmailAddress from = new("admin@site.com", "Eisnt Livros");
            //EmailAddress to = new(email);
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

            //return client.SendEmailAsync(msg);
        }
    }
}
