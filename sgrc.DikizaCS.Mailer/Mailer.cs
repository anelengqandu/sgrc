using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace sgrc.DikizaCS.Mailer
{
    public class Mailer: IMailer
    {
        public MailerResults Send(MailMessage message)
        {
            var credential = new NetworkCredential(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]);
            var client = new SmtpClient(ConfigurationManager.AppSettings["Password"], Convert.ToInt32(ConfigurationManager.AppSettings["Port"]))
            {
                Credentials = credential,
                Host = ConfigurationManager.AppSettings["Host"],
            EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            try
            {
                client.Send(message);
            }
            catch (SmtpFailedRecipientException ex)
            {
                SmtpStatusCode statusCode = ex.StatusCode;
                if (statusCode == SmtpStatusCode.MailboxBusy ||
                    statusCode == SmtpStatusCode.MailboxUnavailable ||
                    statusCode == SmtpStatusCode.InsufficientStorage ||
                    statusCode == SmtpStatusCode.MailboxNameNotAllowed ||
                    statusCode == SmtpStatusCode.SyntaxError ||
                    statusCode == SmtpStatusCode.TransactionFailed)
                {
                    // wait 5 seconds, try a second time
                    Thread.Sleep(5000);
                    client.Send(message);
                }
                else
                {
                    return new MailerResults
                    {
                        Status = "Fail",
                        DescripText = ex.Message,
                        Success = false
                    };

                }

            }
            finally
            {
                message.Dispose();

            }
            return new MailerResults { Status = "Success", DescripText = "" };

        }
    }
}
