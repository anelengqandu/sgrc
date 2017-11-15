using System;
using System.Collections.Generic;
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
using sgrc.DikizaCS.DAL.User;
using sgrc.DikizaCS.DAL.User.Dto;
using sgrc.DikizaCS.DAL.Utils;
using sgrc.DikizaCS.Models;
using sgrc.DikizaCS.Utility;

namespace sgrc.DikizaCS.Areas.API
{
    public class StudentApiController : ApiController
    {
        private readonly IClientAppService _clientRepository;
        private readonly IUserAppService _userRepository;
        public StudentApiController()
        {
            _clientRepository = new ClientAppService();
            _userRepository = new UserAppService();
        }

        [HttpGet]
        [Route("user/api/clients/dtclientstudents/{id}")]
        public object index(int iDisplayStart, int iDisplayLength, string sSearch,long id)
        {
            var context = HttpContext.Current;
            PagingInfo paging = new PagingInfo();
            paging.Skip = iDisplayStart;
            paging.Take = iDisplayLength;
            paging.SearchString = sSearch;


            var data = _clientRepository.GetClientData(ref paging,id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            return jDataTables.jsonObject(data.Students.ToList(), paging.ResultCount);
        }

        [HttpPost]
        [Route("api/student/create")]
        public async Task<DBResult> CreateStudent(RegisterModel user)
        {
            Random ran = new Random();
            var guidlength=ran.Next(4, 10);
            var registerInput = new RegisterInput()
            { 
                ClientId = user.ClientId,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Password = PasswordGenerator.GeneratePassword(guidlength),
                PhoneNumber = user.PhoneNumber,
                TelephoneNumber = user.TelephoneNumber
            };

            DBResult result;
            if (!ModelState.IsValid || user == null)
            {
                return new DBResult { Status = "Fail", DescripText = BadRequest(ModelState).ToString() };

            }

            var exist = db_User.GetByEmail(user.Email);

            if (exist != null)//exist
            {
                return new DBResult { Status = "Fail", DescripText = "Account already axist" };
            }


            result = await _userRepository.CreateOrUpdateUser(registerInput);

            return new DBResult { Status = result.Status, DescripText = result.DescripText };

        }



        [HttpPost]
        [Route("user/api/student/edit")]
        public async Task<DBResult> UpdateUser(StudentModel user)
        {

            var editInput = new RegisterInput()
            { 
                Id=user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                TelephoneNumber = user.TelephoneNumber,

            };

            DBResult result;
            if (!ModelState.IsValid)
            {
                return new DBResult { Status = "Fail", DescripText = BadRequest(ModelState).ToString() };

            }

            result = await _userRepository.CreateOrUpdateUser(editInput);

            return new DBResult { Status = result.Status, DescripText = result.DescripText };

        }


        [HttpPost]
        [Route("user/api/student/activestatus/{id}")]
        public async Task<object>ActiveStatus(long id)
        {
            DBResult result;
            result = await _userRepository.ActiveStatus(id);
            return new DBResult { Status = result.Status, DescripText = result.DescripText, Success = result.Success };
        }

    }
}
