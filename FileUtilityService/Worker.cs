using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileUtilityService
{
    public class Worker : BackgroundService
    {
        PDFFunctions PDFFunctions;
        private readonly ILogger<Worker> _logger;
        public string _inputFolderLocation = WorkerOptions.InputFolderLocation;
        public Worker(ILogger<Worker> logger)
        {
            PDFFunctions = new PDFFunctions();
            _logger = logger;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
               // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //var what = _options.InputFolderLocation;

                var input = _inputFolderLocation;
                var files = Directory.GetFiles(input);

                foreach (var item in files)
                { 
                    if (Path.GetExtension(item) == ".tiff" || Path.GetExtension(item) == ".tif")
                    {
                        string PDFMessage = "";
                        _logger.LogInformation("A compatible file was found: " + Path.GetFileName(item));
                        _logger.LogInformation("Converting: " + Path.GetFileName(item));
                        await File.AppendAllTextAsync(@".\logfile.txt", "Compatible file found: " + item + Environment.NewLine);
                        FileInfo fileInfo = new FileInfo(item);
                        String Filename = System.IO.Path.GetFileNameWithoutExtension(item);

                        //make a copy of the original file in the folder designated from settings - CopyFolderLocation. 
                        File.Copy(item, WorkerOptions.CopyFolderLocation + Filename + " " + DateTime.Now.ToString("dd MMM HH mm ss") + Path.GetExtension(item));

                        //send the file to the Tif to PDF converter (PDFFunctions)
                        try
                        {
                            //Send the file to the PDF converter and return the resulting file path.
                            PDFMessage = PDFFunctions.ConvertTiffToPdf(item) + Environment.NewLine;

                            await File.AppendAllTextAsync(@".\logfile.txt", item + " was converted to: " + PDFMessage);
                            _logger.LogInformation("Converted: " + item + " to " + PDFMessage);
                            //File.Delete(item);

                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation("Failed to Convert: " + item + " | " + ex.Message);
                        }

                        //attempt to send the file as an email to all recepients
                        try
                        {
                            SendMail.Send(PDFMessage.TrimEnd(Environment.NewLine.ToCharArray()));
                        }
                        catch(Exception ex)
                        {
                            _logger.LogInformation(ex.Message);
                        }

                        //attempt to delete the original file
                        try
                        {
                           
                            File.Delete(item);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation("Failed to Delete: " + item + " | " + ex.Message);
                        }


                    }
                }
                //after 2 seconds, run again
                await Task.Delay(2000, stoppingToken);
            }
        }

        public override void Dispose()
        {
            // DO YOUR STUFF HERE
        }
    }
}
