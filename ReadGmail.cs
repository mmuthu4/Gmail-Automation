using System;
using MailKit.Net.Imap;
using MailKit;
using MailKit.Search;
using MimeKit;

class Program
{
    static void Main(string[] args)
    {
        string email = "your-email@gmail.com";
        string appPassword = "your-app-password";

        using (var client = new ImapClient())
        {
            try
            {
                // Connect to Gmail IMAP server
                client.Connect("imap.gmail.com", 993, true);

                // Authenticate
                client.Authenticate(email, appPassword);

                // Access the Inbox
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                Console.WriteLine($"Total messages: {inbox.Count}");

                // Read the latest 5 messages
                for (int i = inbox.Count - 1; i >= Math.Max(inbox.Count - 5, 0); i--)
                {
                    var message = inbox.GetMessage(i);
                    Console.WriteLine("Subject: " + message.Subject);
                    Console.WriteLine("From: " + message.From);
                    Console.WriteLine("Date: " + message.Date);
                    Console.WriteLine("Body Preview: " + message.TextBody?.Substring(0, Math.Min(100, message.TextBody.Length)));
                    Console.WriteLine(new string('-', 50));
                }

                client.Disconnect(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
