using CompanyDAL.Models;
using CompanyPL.Settings;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using System.Net.Mail;


namespace CompanyPL.Helpers
{
    public class EmailSettings : IMailSettings
    {

        // Send Email dont use mailkit
        //public static void SendEmail(Email email)
        //{

        //		var client = new SmtpClient("smtp.gmail.com", 587);
        //		client.EnableSsl = true;
        //		//yoekedylukpnvvzs
        //		client.Credentials = new NetworkCredential("medogadmedo7@gmail.com", "yoekedylukpnvvzs");
        //		client.Send("medogadmedo7@gmail.com", email.To, email.Subject, email.Body);


        //}


        //Send Email  use mailkit
        private  MailSettings options;

        public EmailSettings(IOptions<MailSettings> options)
        {
            this.options = options.Value;
        }
        public void SendMail(Email email)
        {
            //Sender
            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(options.Email),
                Subject = email.Subject,
            };
            //send to who ?
            mail.To.Add(MailboxAddress.Parse(email.To));

            //body
            var builder = new BodyBuilder();
            builder.TextBody = email.Body;
            mail.Body = builder.ToMessageBody();

            mail.From.Add(new MailboxAddress(options.DisplayName, options.Email));

            //open Connection
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(options.Host , options.Port ,SecureSocketOptions.StartTls);
            smtp.Authenticate(options.Email, options.Password);

            smtp.Send(mail);
            smtp.Disconnect(true);
        }
    }
}
