using System.Threading.Tasks;
using sgrc.DikizaCS.DAL.Client.Dto;
using sgrc.DikizaCS.DAL.Utils;

namespace sgrc.DikizaCS.DAL.Client
{
    public interface IClientAppService
    {
        ClientDto GetAll();
        Task<DBResult> CreateOrUpdateClient(CreateOrUpdateInput input);
        ClientOutput GetClientData(ref PagingInfo paging, long? clientId);
        Task<DBResult> AccountConfirmation(string securitystamp);
        int ClientCounter();
    }
}
