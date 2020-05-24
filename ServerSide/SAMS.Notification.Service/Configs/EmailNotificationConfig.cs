namespace SAMS.Notification.Service.Configs
{
    public class EmailNotificationConfig
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Subject { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
