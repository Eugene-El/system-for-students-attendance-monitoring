using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SAMS.Database.EF.EntityFramework;

namespace SAMS.Attendance.Receiving.Service
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

                    services.Configure<ApiPaths>(configuration.GetSection("ApiPaths"));

                    services.AddHostedService<Worker>();
                });
    }
}
