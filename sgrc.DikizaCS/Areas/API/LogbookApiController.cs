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
using sgrc.DikizaCS.DAL.Logbook;
using sgrc.DikizaCS.DAL.Logbook.Dto;
using sgrc.DikizaCS.DAL.Utils;
using sgrc.DikizaCS.Utility;

namespace sgrc.DikizaCS.Areas.API
{
    public class LogbookApiController : ApiController
    {

        private readonly ILogbookAppService _logbookRepository;
        public LogbookApiController()
        {
            _logbookRepository = new LogbookAppService();
        }

        [HttpGet]
        [Route("logbook/api/dtPrograms/{studentId}")]
        public object index(int iDisplayStart, int iDisplayLength, string sSearch, long studentId)
        {
            var context = HttpContext.Current;
            PagingInfo paging = new PagingInfo();
            paging.Skip = iDisplayStart;
            paging.Take = iDisplayLength;
            paging.SearchString = sSearch;


            var data = _logbookRepository.GetByStudent(ref paging, studentId);
            JavaScriptSerializer js = new JavaScriptSerializer();
            return jDataTables.jsonObject(data, paging.ResultCount);
        }


        [HttpPost]
        [Route("logbook/api/create")]
        public object create(LogbookModel logtime)
        {
            if (!ModelState.IsValid || logtime == null)
                throw new FormatException();

            var newLogbook = new LogbookInput()
            {
               
                UserId = this.LoggedInId(),
                ProgramId = this.LoggedInStudentProgramId(),
                ActivityType = logtime.ActivityType,
                TimeHours = logtime.TimeHours,
                TimeMinutes = logtime.TimeMinutes,
                DateOfActivity = logtime.DateOfActivity,
                ActivityDescription = logtime.ActivityDescription,
            };
            var result = _logbookRepository.CreateOrUpdateLogBook(newLogbook);

            return new DBResult { Status = result.Status, DescripText = result.DescripText, Success = result.Success };

        }

        [HttpPost]
        [Route("logbook/api/edit")]
        public object edit(LogbookModel logtime)
        {
            if (!ModelState.IsValid || logtime == null)
                throw new FormatException();

            var newLogbook = new LogbookInput()
            {

                UserId = this.LoggedInId(),
                Id = logtime.Id,
                ActivityType = logtime.ActivityType,
                TimeHours = logtime.TimeHours,
                TimeMinutes = logtime.TimeMinutes,
                DateOfActivity = logtime.DateOfActivity,
                ActivityDescription = logtime.ActivityDescription,
            };
            var result = _logbookRepository.CreateOrUpdateLogBook(newLogbook);

            return new DBResult { Status = result.Status, DescripText = result.DescripText, Success = result.Success };

        }

        [HttpGet]
        [Route("logbook/api/getTime/{id}")]
        public async Task<object> GetTimes(long id)
        {
            var data = await _logbookRepository.GetAll(id);
            return new { data = data };
        }

        [HttpGet]
        [Route("logbook/api/edittime/{id}")]
        public  object GetTime(long id)
        {
            var data =  _logbookRepository.Get(id);
            return new { data = data };
        }

        [HttpPost]
        [Route("logbook/api/activestatus/{id}")]
        public async Task<DBResult> ActiveStatus(long id)
        {
            var result = await _logbookRepository.ActiveStatus(id);
            return new DBResult { Status = result.Status, DescripText = result.DescripText, Success = result.Success };
        }

        [HttpPost]
        [Route("logbook/api/deletestatus/{id}")]
        public async Task<DBResult> DeleteStatus(long id)
        {
            var result = await _logbookRepository.DeleteStatus(id);
            return new DBResult { Status = result.Status, DescripText = result.DescripText, Success = result.Success };
        }
    }
}
