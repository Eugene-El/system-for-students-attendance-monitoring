using Nexmo.Api;
using Nexmo.Api.Request;
using SAMS.Notification.Service.Configs;
using SAMS.Notification.Service.Models;
using System;
using System.Linq;

namespace SAMS.Notification.Service.Notificators
{
    public class SmsNotificator
    {
        private SmsNotificationConfig config;
        public SmsNotificator(SmsNotificationConfig config)
        {
            this.config = config;
        }

        public NotificationResult Notify(string[] to, string message)
        {
            try
            {
                var client = new Client(new Credentials(config.ApiKey, config.ApiSecret));

                foreach (var number in to)
                {
                    var result = client.SMS.Send(new SMS.SMSRequest
                    {
                        from = config.SenderName,
                        to = number,
                        text = message
                    });
                    var error = result.messages.FirstOrDefault().error_text;
                    if (!string.IsNullOrEmpty(error))
                        return new NotificationResult
                        {
                            NotificationStatus = BusinessLogic.Entities.Enumerations.NotificationStatus.Error,
                            ErrorMessage = error
                        };
                }
                return new NotificationResult {
                    NotificationStatus = BusinessLogic.Entities.Enumerations.NotificationStatus.Success
                };
            }
            catch (Exception exception)
            {
                return new NotificationResult
                {
                    NotificationStatus = BusinessLogic.Entities.Enumerations.NotificationStatus.Error,
                    ErrorMessage = exception.Message
                };
            }
        }
    }
}
