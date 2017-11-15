

namespace sgrc.DikizaCS.Mailer
{
    public interface ISendEmail
    {
        void SendMail(string emailTo, string subject, string body);
    }
}
