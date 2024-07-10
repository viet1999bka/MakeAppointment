using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    private readonly SmtpClient _smtpClient;

    public EmailSender()
    {
        _smtpClient = new SmtpClient("smtp.office365.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("vietpt@irismedia.vn", "Prothaem@99"),
            EnableSsl = true,
        };
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("vietpt@irismedia.vn"),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);
            return _smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex) {
            throw;
        }
        
    }
}