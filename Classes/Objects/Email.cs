using DarkEye.Classes.Utils;
using System;
using System.Net.Mail;
using System.Threading;

namespace DarkEye.Classes.Objects
{
    /*
    * Created by Gish_Reloaded on 27/03/2020.
    */
    class Email
    {
        String subject;
        String body;
        Attachment[] attachments;
        Owner owner;

        public Email(String subject, String body, Attachment[] attachments, Owner owner)
        {
            this.subject = subject;
            this.body = body;
            this.attachments = attachments;
            this.owner = owner;
        }

        public void Send() { new Thread(this.SendEmail).Start(); }

        private void SendEmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(owner.from);
                mail.To.Add(owner.to);
                mail.Subject = subject;
                mail.Body = body;

                foreach (Attachment attachment in attachments)
                    mail.Attachments.Add(attachment);

                SmtpServer.Port = owner.port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(owner.from, owner.pass);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                WriteUtils.write("DispathEmail: Sending...");
            }
            catch (Exception ex)
            {
                WriteUtils.writeError("DispathEmail: " + ex.ToString());
            }
        }
    }
}
