using HC.Shared.Application.Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace HC.Shared.Application.Services
{
   
    
    public class SMTPMailService : IMailService
    {
        public MailSettingsOptions _mailSettings { get; }

        public SMTPMailService(IOptions<MailSettingsOptions> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendAsync(MailRequest request)
        {
            MailMessage mail = new()
            {
                From = new MailAddress(_mailSettings.From),
                Subject = request.Subject,
                Body = request.Body,
                IsBodyHtml = true
            };
            mail.To.Add(request.To);

            SmtpClient smtpServer = new SmtpClient(_mailSettings.Host)
            {
                Port = _mailSettings.Port,
                Credentials = new System.Net.NetworkCredential(_mailSettings.UserName, _mailSettings.Password),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            await smtpServer.SendMailAsync(mail);
        }
    }
  
}