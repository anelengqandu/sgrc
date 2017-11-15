using System.Net.Mail;

namespace sgrc.DikizaCS.Mailer
{
    public interface IMailer
    {
        MailerResults Send(MailMessage message);
    }
}
