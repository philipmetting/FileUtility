using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace FileUtilityService
{
    public static class SendMail
    {
        public static void Send(string to, string attachment)
        {
            using(SmtpClient smtpClient = new SmtpClient("smtprelay.ernest.health.inc"))
            {
                //Get the from email from the settings
                MailAddress From = new MailAddress(WorkerOptions.FromEmail, WorkerOptions.FromEmail);
                //create the to address from the variables
                MailAddress To = new MailAddress(to, to);
                //create a new mail message based on the from and to
                MailMessage mail = new MailMessage(From, To);
                //Fill in the subject/body from the settings
                mail.Subject = WorkerOptions.EmailSubject;
                mail.Body = WorkerOptions.EmailBody;
                //turn the mailmessage to HTML to future proof
                mail.IsBodyHtml = true;
                //create an attachment based on the variable and tell the application what kind of file it is
                Attachment attachment1 = new Attachment(attachment, MediaTypeNames.Application.Octet);
                mail.Attachments.Add(attachment1);

                try
                {
                    //try to send the email and dispose of the attachment in memory
                    smtpClient.Send(mail);
                    attachment1.Dispose();
                }
                catch(Exception ex)
                {
                    string exception = ex.Message;
                    attachment1.Dispose();
                }
                
            }
        }
    }
}
