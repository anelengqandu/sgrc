using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using sgrc.DikizaCS.DAL;
using sgrc.DikizaCS.DAL.User.Dto;
using sgrc.DikizaCS.DAL.Utils;

namespace sgrc.DikizaCS.API
{
   public class UserApi
    {

        [HttpPost]
        [Route("api/user/createorupdate")]
        public async Task<DBResult> CreateOrUpdateUser(RegisterInput user)
        {
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


            result = await _userRepository.CreateOrUpdateUser(user);




            return new DBResult { Status = result.Status, DescripText = result.DescripText };

        }
    }
}
