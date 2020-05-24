using MailKit.Net.Smtp;
using MimeKit;
using SAMS.Notification.Service.Configs;
using SAMS.Notification.Service.Models;
using System;
using System.Linq;

namespace SAMS.Notification.Service.Notificators
{
    public class EmailNotificator
    {
        protected EmailNotificationConfig config;
        public EmailNotificator(EmailNotificationConfig config)
        {
            this.config = config;
        }

        public NotificationResult Notify(string[] to, string message)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    var emailMessage = new MimeMessage();
                    emailMessage.From.Add(new MailboxAddress(config.Username));
                    emailMessage.To.AddRange(to.Select(t => new MailboxAddress(t)));
                    emailMessage.Subject = config.Subject;
                    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
                    {
                        Text = message
                    };

                    client.Connect(config.SmtpServer, config.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(config.Username, config.Password);

                    client.Send(emailMessage);
                }
                catch (Exception exception)
                {
                    return new NotificationResult
                    {
                        NotificationStatus = BusinessLogic.Entities.Enumerations.NotificationStatus.Error,
                        ErrorMessage = exception.Message
                    };
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
                return new NotificationResult {
                    NotificationStatus = BusinessLogic.Entities.Enumerations.NotificationStatus.Success
                };
            }

        }
    }
}
