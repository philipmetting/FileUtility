using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileUtilityService
{
    public class FileHistory : BackgroundService
    {
        private readonly ILogger<FileHistory> _logger;
        public FileHistory(ILogger<FileHistory> logger)
        {
            _logger = logger;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            // DO YOUR STUFF HERE
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            // DO YOUR STUFF HERE
            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            foreach (var file in Directory.GetFiles(WorkerOptions.CopyFolderLocation))
            {
                DateTime creation = File.GetCreationTime(file);
                if(creation < DateTime.Now.AddDays(-WorkerOptions.DaysToKeepCopy))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation("Failed to Delete: " + file + " | " + ex.Message);
                    }
                }
            }

            //wait 6 hours
            await Task.Delay(1000 * 12, stoppingToken);
        }
    }
}
