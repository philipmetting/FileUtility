using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUtility
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PDFFunctions pDFFunctions = new PDFFunctions();
            //pDFFunctions.ConvertTiffToPdf();
            WorkerOptions.FillOptions();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                IConfiguration configuration = hostContext.Configuration;


                services.AddHostedService<Worker>();
            });
    }
}
