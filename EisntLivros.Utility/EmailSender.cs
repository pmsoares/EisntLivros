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
            //MimeMessage emailToSend = new();
            //emailToSend.From.Add(MailboxAddress.Parse("noreply@pedrosoares.com"));
            //emailToSend.To.Add(MailboxAddress.Parse(email));
            //emailToSend.Subject = subject;
            //emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            ////Send Email
            //using (SmtpClient emailClient = new())
            //{
            //    emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            //    emailClient.Authenticate("pmsoares@gmail.com", "aoiqpguqbimrukvp");
            //    emailClient.Send(emailToSend);
            //    emailClient.Disconnect(true);
            //}
            //return Task.CompletedTask;

            SendGridClient client = new(SendGridSecret);
            EmailAddress from = new("pedro.soares@cherrycovilha.pt", "Eisnt Livros");
            EmailAddress to = new(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

            return client.SendEmailAsync(msg);
        }
    }
}
