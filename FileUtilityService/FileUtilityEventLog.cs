using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FileUtilityService
{
    public static class FileUtilityEventLog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="entryType">Information, Error, Warning</param>
        public static void WriteTo(string message, string entryType)
        {
            string eventLogSource = "FileUtility";
            if (!EventLog.SourceExists(eventLogSource))
            {
                //Uncomment the below line when buiding for release. 
                //EventLog.CreateEventSource(eventLogSource, "FaxFileUtilityService");
            }

            EventLog eventLog = new EventLog();
            eventLog.Source = eventLogSource;

            switch(entryType)
            {
                case "Information":
                    eventLog.WriteEntry(message, EventLogEntryType.Information);
                    break;
                case "Error":
                    eventLog.WriteEntry(message, EventLogEntryType.Error);
                    break;
                case "Warning":
                    eventLog.WriteEntry(message, EventLogEntryType.Warning);
                    break;
                default:
                    break;
            }
        }
    }
}
