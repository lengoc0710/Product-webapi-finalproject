using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using ProductsManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Services
{
   
        public class MailSettings
        {
            public string Mail { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Host { get; set; }
            public int Port { get; set; }

        }

        public interface ISendMailService
        {
            Task SendMail(MailSubmit mailContent);

            Task SendEmailAsync(string email, string subject, string htmlMessage);
        }

        // Dịch vụ gửi mail
        public class SendMailService : ISendMailService
        {
            private readonly MailSettings mailSettings;

            private readonly ILogger<SendMailService> logger;

            // mailSetting -> Inject
            // inject Logger
            public SendMailService(IOptions<MailSettings> _mailSettings, ILogger<SendMailService> _logger)
            {
                mailSettings = _mailSettings.Value;
                logger = _logger;
                logger.LogInformation("Create SendMailService");
            }

            public async Task SendEmailAsync(string email, string subject, string htmlMessage)
            {
                await SendMail(new MailSubmit()
                {
                    To = email,
                    Subject = subject,
                    Body = htmlMessage
                });
            }

            // submit email(MailSubmit)
            public async Task SendMail(MailSubmit mailContent)
            {
                var email = new MimeMessage();
                email.Sender = new MailboxAddress(mailSettings.UserName, mailSettings.Mail);
                email.From.Add(new MailboxAddress(mailSettings.UserName, mailSettings.Mail));
                email.To.Add(MailboxAddress.Parse(mailContent.To));
                email.Subject = mailContent.Subject;


                var builder = new BodyBuilder();
                builder.HtmlBody = mailContent.Body;
                email.Body = builder.ToMessageBody();

                using var smtp = new MailKit.Net.Smtp.SmtpClient();

                try
                {
                    smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                    smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
                    await smtp.SendAsync(email);
                }
                catch (Exception ex)
                {
                    // Save to SaveMail 
                    System.IO.Directory.CreateDirectory("mailssave");
                    var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                    await email.WriteToAsync(emailsavefile);

                    logger.LogInformation("failed email submission, save to - " + emailsavefile);
                    logger.LogError(ex.Message);
                }

                smtp.Disconnect(true);

                logger.LogInformation("send mail to " + mailContent.To);

            }
        
    }
}
