using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.Encodings;

namespace FileUtilityService
{
    class PDFFunctions
    {
        //string LocalPath = @"c:\temp\sample.tiff";

        public string ConvertTiffToPdf(string filename)
        {

            
            try
            {
                PdfDocument s_document = new PdfDocument();
                int pageCount = 0;
                Image Tiff;
                String Filename = System.IO.Path.GetFileNameWithoutExtension(filename);

                //open the file stream and get it ready for auto destruction
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    //import an image from the filestream as a tif
                    Tiff = Image.FromStream(fs);

                    //get page count of tiff
                    pageCount = Tiff.GetFrameCount(FrameDimension.Page);

                    for (int a = 0; a < pageCount; a++)
                    {
                        PdfPage pageNew = s_document.AddPage();
                        Tiff.SelectActiveFrame(FrameDimension.Page, a);
                        XGraphics gfxTiff = XGraphics.FromPdfPage(pageNew);
                        //XImage image = XImage.FromStream
                        XImage image = XImage.FromStream(StreamUtils.ToStream(Tiff, ImageFormat.Tiff));
                        gfxTiff.DrawImage(image, 0, 0);
                    }

                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    //remove old file extension
                }
                string newfile = Filename;

                //create the new file name to include day month hour minute and second of creation to avoid conflicts if necessary
                string savePath = WorkerOptions.OutputFolderLocation + newfile+ " " + DateTime.Now.ToString("dd MMM HH mm ss") + ".pdf";

                s_document.Save(savePath);
                s_document.Close();
                //File.Delete(filename);
                
                return savePath;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

        }


        



    }


}
