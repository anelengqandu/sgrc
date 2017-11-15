using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sgrc.DikizaCS.DAL.Logbook.Dto;
using sgrc.DikizaCS.DAL.Utils;

namespace sgrc.DikizaCS.DAL.Logbook
{
    public interface ILogbookAppService
    {
        LogbookDto Get(long id);
        List<LogbookDto> GetByStudent(ref PagingInfo paging,long studentId);
        DBResult CreateOrUpdateLogBook(LogbookInput logtime);
        Task<List<LogbookDto>> GetAll(long loggedInUserId);
        Task<DBResult> ActiveStatus(long id);
        Task<DBResult> DeleteStatus(long id);
    }
}
