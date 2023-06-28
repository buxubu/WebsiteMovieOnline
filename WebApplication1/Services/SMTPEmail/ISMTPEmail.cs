using WebMovieOnline.Helper;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace WebMovieOnline.Services.SMTPEmail
{
    public interface ISMTPEmail
    {
        Task Send(string mailTo, string subject, string body);
    }
    public class RepoSMTPEmail : ISMTPEmail
    {
        public async Task Send(string mailTo, string subject, string body) 
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("vanphuc131002@gmail.com"));
            email.To.Add(MailboxAddress.Parse(mailTo));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = body };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("phucsieupham13102002@gmail.com", "lyljocpohavmmlnc");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
