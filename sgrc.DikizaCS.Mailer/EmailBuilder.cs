using System.Configuration;
using System.IO;
using sgrc.DikizaCS.Mailer.Model;
using System.Web.Hosting;

namespace sgrc.DikizaCS.Mailer
{
    public class EmailBuilder: IEmailBuilder
    {
        private readonly SendEmail _sendServiceRepository;

       public EmailBuilder()
       {
            _sendServiceRepository = new SendEmail();
        }

       public void OnRegisterClient(AccountRegistration input)
        {

            string body;
            string imagePath = $"{ConfigurationManager.AppSettings["EnableSsl"]}/Resources/images/dikizacs.png";
           var webRootPath = System.Web.HttpContext.Current.Server.MapPath("~");
    
            using (
                var reader =
                    new StreamReader(Path.GetFullPath(Path.Combine(webRootPath, "../sgrc.DikizaCS.Mailer/htmlTemplates/RegisterClient.html")))
                )

                body = reader.ReadToEnd();
            body = body.Replace("{HeaderLogo}", imagePath);
            body = body.Replace("{ClientName}", input.Name+" "+input.Surname);
            body = body.Replace("{ClientEmail}", input.Email);
            body = body.Replace("{Password}", input.Password);
            body = body.Replace("{Url}", input.Url);
           // body = body.Replace("{WebReference}", input.WebReference);
            _sendServiceRepository.SendMail(input.Email, $"no-reply", body);
        }

        public void OnRegisterStudent(AccountRegistration input)
        {
            string body;
            string imagePath = $"{ConfigurationManager.AppSettings["EnableSsl"]}/Resources/images/dikizacs.png";

            using (
                StreamReader reader =
                    new StreamReader(
                        System.Web.HttpContext.Current.Server.MapPath("../sgrc.DikizaCS.Mailer/htmlTemplates/RegisterStudent.html")))
                body = reader.ReadToEnd();
            body = body.Replace("{HeaderLogo}", imagePath);
            body = body.Replace("{StudentName}", input.Name + " " + input.Surname);
            body = body.Replace("{StudentEmail}", input.Email);
            body = body.Replace("{Password}", input.Password);
            _sendServiceRepository.SendMail(input.Email, $"no-reply", body);
        }
    }
}
