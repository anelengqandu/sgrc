using sgrc.DikizaCS.DAL.User;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using sgrc.DikizaCS.Areas.User.Models;
using sgrc.DikizaCS.DAL;
using sgrc.DikizaCS.DAL.User.Dto;
using sgrc.DikizaCS.DAL.Utils;
using sgrc.DikizaCS.Models;

namespace sgrc.DikizaCS.API
{
   
    public class AccountApiController : ApiController
    {
        private readonly IUserAppService _userRepository;

        public AccountApiController()
        {
            _userRepository = new UserAppService();
        }

        [HttpPost]
        [Route("api/user/createorupdate")]
        public async Task<DBResult> CreateOrUpdateUser(RegisterModel user)
        {

            var registerInput = new RegisterInput()
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Password = user.Password
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
        [Route("api/user/activestatus/{id}")]
        public async Task<DBResult> ActiveStatus(long id)
        {
            var result =await _userRepository.ActiveStatus(id);
            return new DBResult { Status = result.Status, DescripText = result.DescripText, Success = result.Success };
        }
    }
}
