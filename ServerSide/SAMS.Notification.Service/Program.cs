using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SAMS.Database.EF.EntityFramework;
using SAMS.Notification.Service.Configs;

namespace SAMS.Notification.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseWindowsService() // For Windows Service
                //.UseSystemd() // For Linux Deamon
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;

                    services.AddDbContextPool<DataContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("StudentAttendanceDB")));

                    services.AddSingleton(configuration
                        .GetSection(nameof(EmailNotificationConfig))
                        .Get<EmailNotificationConfig>());

                    services.AddSingleton(configuration
                        .GetSection(nameof(SmsNotificationConfig))
                        .Get<SmsNotificationConfig>());

                    services.AddHostedService<Worker>();
                });
    }
}
