using System;
using System.Net;
using System.Net.Mail;
using System.IO;

class EmailSender
{
    static void Main(string[] args)
    {
        string fromEmail = "your-email@gmail.com";
        string toEmail = "recipient@example.com";
        string subject = "Test Email with Attachment";
        string body = "This is a test email with a file attachment.";
        string smtpHost = "smtp.gmail.com";
        int smtpPort = 587; // or 465 for SSL
        string password = "your-app-password"; // App password for Gmail
        string attachmentPath = @"C:\path\to\your\file.pdf";

        try
        {
            MailMessage mail = new MailMessage(fromEmail, toEmail, subject, body);

            // Add attachment if file exists
            if (File.Exists(attachmentPath))
            {
                Attachment attachment = new Attachment(attachmentPath);
                mail.Attachments.Add(attachment);
            }
            else
            {
                Console.WriteLine("Attachment file not found: " + attachmentPath);
                return;
            }

            SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true
            };

            smtpClient.Send(mail);
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
        }
    }
}
