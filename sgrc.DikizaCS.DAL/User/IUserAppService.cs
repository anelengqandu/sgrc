using System.Threading.Tasks;
using sgrc.DikizaCS.DAL.User.Dto;
using sgrc.DikizaCS.DAL.Utils;

namespace sgrc.DikizaCS.DAL.User
{
    public interface IUserAppService
    {
        Task<DBResult> CreateOrUpdateUser(RegisterInput input);
        Task<UserAuthOutPut> Login(LoginInput input);
        Task<DBResult> ActiveStatus(long id);
        UserDto GetByClient(long clientUserId);
    }
}
