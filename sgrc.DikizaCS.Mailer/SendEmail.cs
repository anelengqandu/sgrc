using System.Configuration;
using System.Net.Mail;

namespace sgrc.DikizaCS.Mailer
{
    public class SendEmail: ISendEmail
    {
        private readonly IMailer _mailerServiceRepository;
        public SendEmail()
        {
            _mailerServiceRepository = new Mailer();
        }
        public  void SendMail(string emailTo,string subject, string body)
        {
           
            var from = new MailAddress(ConfigurationManager.AppSettings["DefaultEmail"]);
            var to = new MailAddress(emailTo);

            var mailMessage = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            _mailerServiceRepository.Send(mailMessage);
        }

    }
}
