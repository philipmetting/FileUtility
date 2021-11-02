using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileUtilityService
{
    public static class WorkerOptions
    {
        public static string InputFolderLocation;
        public static string OutputFolderLocation;
        public static string CopyFolderLocation;
        public static string DaysToKeepCopy;
        public static string UsersToEmail;
        public static string FromEmail;
        public static string EmailBody;
        public static string EmailSubject;
        public static List<string> emailAddresses;

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
                if (line.Split('|')[0] == "CopyFolderLocation")
                {
                    setCopyFolderLocation(line.Split('|')[1]);
                }
                if (line.Split('|')[0] == "DaysToKeepCopy")
                {
                    setDaysToKeepCopy(line.Split('|')[1]);
                }
                if (line.Split('|')[0] == "UsersToEmail")
                {
                    setUsersToEmail(line.Split('|')[1]);
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
        public static void setCopyFolderLocation(string copyfolderlocation)
        {
            CopyFolderLocation = copyfolderlocation;
        }
        public static void setDaysToKeepCopy(string daysToKeepCopy)
        {
            DaysToKeepCopy = daysToKeepCopy;
        }
        public static void setEmailSubject(string emailSubject)
        {
            EmailSubject = emailSubject;
        }
        public static void setEmailBody(string emailBody)
        {
            EmailBody = emailBody;
        }
        public static void setFromEmail(string fromEmail)
        {
            FromEmail = fromEmail;
        }
        public static void setUsersToEmail(string usersToEmail)
        {
            emailAddresses = new List<string>();
            UsersToEmail = usersToEmail;
            
            if(!string.IsNullOrEmpty(UsersToEmail))
            {
                foreach (var email in UsersToEmail.Split(';'))
                {
                    if(!string.IsNullOrEmpty(email))
                        emailAddresses.Add(email);
                }
            }
        }
    }
}
