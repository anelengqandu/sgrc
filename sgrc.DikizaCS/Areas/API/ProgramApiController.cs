using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using sgrc.DikizaCS.Areas.User.Models;
using sgrc.DikizaCS.DAL.Program;
using sgrc.DikizaCS.DAL.Program.Dto;
using sgrc.DikizaCS.DAL.Utils;
using sgrc.DikizaCS.Utility;

namespace sgrc.DikizaCS.Areas.API
{
    public class ProgramApiController : ApiController
    {
        private readonly IProgramAppService _programRepository;
        public ProgramApiController()
        {
            _programRepository = new ProgramAppService();
        }

        [HttpGet]
        [Route("user/api/program/dtPrograms")]
        public object index(int iDisplayStart, int iDisplayLength, string sSearch)
        {
            var context = HttpContext.Current;
            PagingInfo paging = new PagingInfo();
            paging.Skip = iDisplayStart;
            paging.Take = iDisplayLength;
            paging.SearchString = sSearch;


            var data = _programRepository.GetPrograms(ref paging);
            JavaScriptSerializer js = new JavaScriptSerializer();
            return jDataTables.jsonObject(data, paging.ResultCount);
        }

        [HttpGet]
        [Route("user/api/program/dtPrograms/{clientId}")]
        public object index(int iDisplayStart, int iDisplayLength, string sSearch,long clientId)
        {
            var context = HttpContext.Current;
            PagingInfo paging = new PagingInfo();
            paging.Skip = iDisplayStart;
            paging.Take = iDisplayLength;
            paging.SearchString = sSearch;


            var data = _programRepository.GetClientPrograms(ref paging,clientId);
            JavaScriptSerializer js = new JavaScriptSerializer();
            return jDataTables.jsonObject(data, paging.ResultCount);
        }

        [HttpPost]
        [Route("user/api/program/create")]
        public object Create(ProgramModel program)
        {
            if (!ModelState.IsValid || program == null)
                throw new FormatException();

            var newProgram = new CreateOrUpdateInput()
            {
                Name = program.Name,
                Description = program.Description,
                
            };
            var result = _programRepository.CreateOrUpdateProgram(newProgram);

            return new DBResult { Status = result.Result.Status, DescripText = result.Result.DescripText, Success = result.Result.Success };

        }

        [HttpPost]
        [Route("user/api/program/edit")]
        public object Edit(ProgramModel program)
        {
            if (!ModelState.IsValid || program == null)
                 throw new FormatException();

            var oldProgram = new CreateOrUpdateInput()
            {
                 Id = program.Id,
                Name = program.Name,
                Description = program.Description,
                ModifiedByUser = this.LoggedInId(),
               // IsActive = program.IsActive


            };
            var result = _programRepository.CreateOrUpdateProgram(oldProgram);

            return new DBResult { Status = result.Result.Status, DescripText = result.Result.DescripText, Success = result.Result.Success };

        }

        [HttpPost]
        [Route("user/api/program/activestatus/{Id}")]
        public async Task<DBResult> ActiveStatus(long id)
        {
            var result = await _programRepository.ActiveStatus(id);
            return new DBResult { Status = result.Status, DescripText = result.DescripText, Success = result.Success };
        }
    }
}
