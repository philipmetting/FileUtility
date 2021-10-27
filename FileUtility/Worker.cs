using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PdfSharp;
using System.IO;

namespace FileUtility
{
    public class Worker : BackgroundService
    {
        PDFFunctions PDFFunctions;
        private readonly ILogger<Worker> _logger;
        public string _inputFolderLocation = WorkerOptions.InputFolderLocation;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            PDFFunctions = new PDFFunctions();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //var what = _options.InputFolderLocation;

                var input = _inputFolderLocation;
                var files = Directory.GetFiles(input);

                foreach(var item in files)
                {
                    if(Path.GetExtension(item) == ".tiff" || Path.GetExtension(item) == ".tif")
                    {
                        _logger.LogInformation("Converting: " + item);
                        await File.AppendAllTextAsync(@".\logfile.txt", "Compatible file found: " + item + Environment.NewLine);
                        try
                        {
                            string PDFMessage = PDFFunctions.ConvertTiffToPdf(item) + Environment.NewLine;
                            await File.AppendAllTextAsync(@".\logfile.txt", item + " was converted to: " + PDFMessage);
                            _logger.LogInformation("Converted: " + item + " to " + PDFMessage);
                            //File.Delete(item);
                            
                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation("Failed to Convert: " + item + " | " + ex.Message);
                        }
                        try
                        {
                            /*FileStream stream = new FileStream(item, FileMode.Open, FileAccess.Read, FileShare.None, 1, FileOptions.DeleteOnClose | FileOptions.Asynchronous);
                            using (stream)
                            {
                                await stream.FlushAsync();
                                _logger.LogInformation("Attempting to delete: " + item);
                            }*/
                            File.Delete(item);
                        }
                        catch(Exception ex)
                        {
                            _logger.LogInformation("Failed to Delete: " + item + " | " + ex.Message);
                        }
                        

                    }
                }
                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
