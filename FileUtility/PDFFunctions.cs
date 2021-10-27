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

namespace FileUtility
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
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    Tiff = Image.FromStream(fs);


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

                    string savePath = WorkerOptions.OutputFolderLocation + newfile + ".pdf";

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
