using System.Net;
using System.Net.Mail;


namespace employee_system
{

    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {

            string mail = "ahmedahmed94044@gmail.com";
            string pw = "vhtyrxhjaacupyrn";
            var client = new SmtpClient("smtp.gmail.com.", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            return client.SendMailAsync(
                new MailMessage(from: mail,
                                to: email,
                                subject,
                                message
                                ));
        }
    }
}