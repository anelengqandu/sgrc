using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sgrc.DikizaCS.DAL.Program.Dto;
using sgrc.DikizaCS.DAL.Utils;

namespace sgrc.DikizaCS.DAL.Program
{
    public  interface IProgramAppService
    {
        Task<DBResult> CreateOrUpdateProgram(CreateOrUpdateInput input);
        List<ProgramDto> GetPrograms(ref PagingInfo paging);
        List<ProgramDto> GetClientPrograms(ref PagingInfo paging, long? clientId);
        Task<DBResult> ActiveStatus(long id);
        ProgramDto GetAsync(long id);
        int ProgramsCounter();
    }
}
