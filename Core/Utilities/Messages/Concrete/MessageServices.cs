using Core.Utilities.Messages.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Messages.Concrete
{
    public class MessageServices : IMessageService
    {
        private readonly IConfiguration _configuration;

        public MessageServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMessage(string to, string subject, string message)
        {
            MailMessage mailMessage = new()
            {
                IsBodyHtml = true
            };

            mailMessage.To.Add(to);
            mailMessage.Body = message;
            mailMessage.Subject = subject;
            mailMessage.From = new(_configuration["EmailSettings:Email"], "Someverything", System.Text.Encoding.UTF8);

            SmtpClient smtpClient = new()
            {
                Port = Convert.ToInt32(_configuration["EmailSetting:Port"]),
                EnableSsl = true,
                Host = _configuration["EmailSettings:Host"],
                Credentials = new NetworkCredential(_configuration["EmailSettings:Email"], _configuration["EmailSettings:Password"])
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
