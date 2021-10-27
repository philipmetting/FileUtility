using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileUtility
{
    public static class WorkerOptions
    {
        public static string InputFolderLocation;
        public static string OutputFolderLocation;

        public static void FillOptions()
        {

            foreach(var line in File.ReadAllLines(@".\settings.hs"))
            {
                if(line.Split('|')[0] == "InputFolderLocation")
                {
                    setInputFolderLocation(line.Split('|')[1]);
                }
                if (line.Split('|')[0] == "OutputFolderLocation")
                {
                    setOutpoutFolderLocation(line.Split('|')[1]);
                }
            }

            if(!Directory.Exists(InputFolderLocation))
            {
                Directory.CreateDirectory(InputFolderLocation);
            }
            
            
        }

        public static void setInputFolderLocation(string inputfolderlocation)
        {
            InputFolderLocation = inputfolderlocation;
        }
        public static void setOutpoutFolderLocation (string outputfolderlocation)
        {
            OutputFolderLocation = outputfolderlocation;
        }
    }
}
