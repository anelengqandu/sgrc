using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using sgrc.DikizaCS.Areas.User.Models;
using sgrc.DikizaCS.DAL;
using sgrc.DikizaCS.DAL.Client;
using sgrc.DikizaCS.DAL.Client.Dto;
using sgrc.DikizaCS.DAL.Utils;
using sgrc.DikizaCS.Mailer;
using sgrc.DikizaCS.Mailer.Model;
using sgrc.Encrypt;
using sgrc.DikizaCS.SMS;
using sgrc.DikizaCS.SMS.Model;
using sgrc.DikizaCS.Utility;

namespace sgrc.DikizaCS.Areas.API
{
    public class ClientApiController : ApiController
    {
        private readonly IClientAppService _clientRepository;
        private readonly IEmailBuilder _emailerServiceRepository;
        private readonly ISmsSender _smserServiceRepository;
        public ClientApiController()
        {
            _smserServiceRepository = new SmsSender();
            _clientRepository = new ClientAppService();
            _emailerServiceRepository = new EmailBuilder();
        }

        [HttpPost]
        [Route("user/api/client/create")]
        public object Create(ClientModel client)
        {
            bool hasError = false;
            Random ran = new Random();
            var guidlength = ran.Next(4, 10);
            if (!ModelState.IsValid || client == null)
                throw new FormatException();

           

            var newClient = new CreateOrUpdateInput()
            {
                Name = client.Name,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                Password = PasswordGenerator.GeneratePassword(guidlength),
                SecurityStampId = PasswordManager.SecurityToken(Guid.NewGuid().ToString("N").Replace("+", "").Replace(" ", "")),
                ContactName = client.ContactName,
                ContactSurname = client.ContactSurname,
                ContactPhone = client.ContactPhone,
                ContactEmail = client.ContactEmail

            };

            var qClient = db_Client.GetByEmail(newClient.ContactEmail);
            var qUser = db_User.GetByEmail(newClient.ContactEmail);
                       

            if (qUser != null || qClient != null)
            {
                hasError = true;
                return new DBResult
                {
                    Status = !hasError ? "Success" : "Fail",
                    DescripText = "Account with same email already exist",
                    Success = false
                };
            }
           


            var result = _clientRepository.CreateOrUpdateClient(newClient);

            if (result.Result.Status == "Success")
            {
                var emailInput = new AccountRegistration
                {
                    Name = client.ContactName,
                    Surname = client.ContactSurname,
                    Password = newClient.Password,
                    Url = ConfigurationManager.AppSettings["BaseUrl"] + "/Auth?secureAuthenticationToken=" + newClient.SecurityStampId,
                    Email = client.ContactEmail,

                };
                var smsInput = new MessageModel
                {
                    Number = newClient.ContactPhone,
                    Message = "Hi"+ " " + client.ContactName+" "+ client.ContactSurname+ ". You are currently registered as a client in DikizaCS Admin Portal, to activate your account please login to the email address." + client.ContactEmail
                };
                try
                {
                    _emailerServiceRepository.OnRegisterClient(emailInput);
                    _smserServiceRepository.Send(smsInput);

                }
                catch (Exception ex)
                {
                    db_Client getClient = db_Client.GetByEmail(newClient.ContactEmail);
               
                    var r=getClient._remove();
                    if (!r.Success)
                    {
                        hasError = true;
                        return new DBResult
                        {
                            Status = !hasError ? "Success" : "Fail",
                            DescripText = ex.Message,
                            Success = false
                        };
                    }
                    if (!hasError)
                    {
                        DataAccess.SaveChanges();
                        if (ex.InnerException != null)
                        {
                            return new DBResult
                            {
                                Status = !hasError ? "Success" : "Fail",
                                DescripText = ex.InnerException.Message,
                                Success = false
                            };
                        }
                        else
                        {
                            return new DBResult
                            {
                                Status = !hasError ? "Success" : "Fail",
                                DescripText = ex.Message,
                                Success = false
                            };
                        }
                       
                    }
                }

            }

            return new DBResult { Status = result.Result.Status, DescripText = result.Result.DescripText, Success = result.Result.Success };

        }

        [HttpPost]
        [Route("user/api/client/edit")]
        public object Edit(ClientModel client)
        {
            if (!ModelState.IsValid || client == null)
                throw new FormatException();

            var newClient = new CreateOrUpdateInput()
            {
                Id = client.Id,
                Name = client.Name,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                IsActive = client.IsActive,
                ContactName = client.ContactName,
                ContactSurname = client.ContactSurname,
                ContactPhone = client.ContactPhone,
                ContactEmail = client.ContactEmail

            };
            var result = _clientRepository.CreateOrUpdateClient(newClient);

            return new DBResult { Status = result.Result.Status, DescripText = result.Result.DescripText, Success = result.Result.Success };

        }

      
        [HttpGet]
        [Route("api/getclients/{sSearch?}")]
        public object Clients(string sSearch)
        {

           var  result =  _clientRepository.GetAll(sSearch);
            return result.ClientList.Count > 0 ? new DBResult { Status = "Success", DescripText = "", Success = true,Object = result.ClientList} : new DBResult { Status = "Fail", DescripText = "No data found", Success = false };
        }
    }
}
