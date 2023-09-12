using System.Net.Mail;
using API.DTOs;
using API.Interfaces.Services;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace API.Services
{
    public class MailService : IMailService
    {
        private readonly EmailSettings _settings;

        public MailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }
        public async Task SendEmailAsync(EmailMessage mail)
        {
            List<MimeMessage> messages = new List<MimeMessage>();

            var emailMessage = new MimeMessage
            {
                Sender = new MailboxAddress(_settings.SendersName, _settings.SmtpUserName),
                Subject = mail.Subject
            };
            emailMessage.From.Add(new MailboxAddress(_settings.EmailDisplayName, _settings.SmtpUserName));

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = mail.Content };

            emailMessage.To.Add(new MailboxAddress(mail.MailTo, mail.MailTo));

            messages.Add(emailMessage);
            

            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    var socketOptions = _settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;
                    await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpServerPort, socketOptions);

                    if (!string.IsNullOrEmpty(_settings.SmtpUserName))
                    {
                        await smtp.AuthenticateAsync(_settings.SmtpUserName, _settings.SmtpPassword);
                    }

                    foreach (var item in messages)
                    {
                        await smtp.SendAsync(item);
                    }

                    await smtp.DisconnectAsync(true);
                }
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }

    }
}