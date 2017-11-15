using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using sgrc.DikizaCS.DAL.Logbook.Dto;
using sgrc.DikizaCS.DAL.Utils;
using sgrc.DikizaCS.DAL;

namespace sgrc.DikizaCS.DAL.Logbook
{
   public class LogbookAppService:ILogbookAppService
    {
       public LogbookDto Get(long id)
       {
            var q = from rowC in DataAccess.metadata.db_LogBook
                    where rowC.Id==id && !rowC.IsDeleted
                    orderby rowC.CreationDateTime descending
                    select new LogbookDto()
                    {
                        Id = rowC.Id,
                        DateOfActivity = rowC.DateOfActivity,
                        WeekStartDate = rowC.WeekStartDate,
                        WeekEndDate = rowC.WeekEndDate,
                        TimeHours = rowC.TimeHours,
                        TimeMinutes = rowC.TimeMinutes,
                        WeekNumber = rowC.WeekNumber,
                        ActivityDescription = rowC.ActivityDescription,
                        ActivityType = rowC.ActivityType,
                        IsSubmited = rowC.IsSubmited,
                    };
          
            var fcount = q;
            return fcount.FirstOrDefault();

        }

       public List<LogbookDto> GetByStudent(ref PagingInfo paging,long studentId)
        {
        
           var q = from rowC in DataAccess.metadata.db_LogBook
                   where rowC.UserId == studentId
                   orderby rowC.CreationDateTime descending
                   select new LogbookDto()
                   {
                       Id = rowC.Id,
                       ProgramId = rowC.ProgramId,
                       DateOfActivity = rowC.DateOfActivity,
                       UserId = rowC.UserId,
                       WeekStartDate = rowC.WeekStartDate,
                       WeekEndDate = rowC.WeekEndDate,
                       TimeHours = rowC.TimeHours,
                       TimeMinutes = rowC.TimeMinutes,
                       WeekNumber = rowC.WeekNumber,
                       ActivityDescription = rowC.ActivityDescription,
                       ActivityType = rowC.ActivityType,
                       IsSubmited = rowC.IsSubmited,
                       CreationDateTime = rowC.CreationDateTime
                   };

            if (!string.IsNullOrEmpty(paging.SearchString))
            {
                var searchString = paging.SearchString;
                q = from row in q
                                  where row.ActivityDescription.Contains(searchString)
                                  select row;
            }

            var fq = q.Future().Skip(paging.Skip).Take(paging.Take);
            var fcount = q.FutureCount();
            paging.ResultCount = fcount.Value;
           
            return fq.ToList();
           
        }
        public async Task<List<LogbookDto>> GetAll(long loggedInUserId)
        {
            var q = from rowC in DataAccess.metadata.db_LogBook
                    where !rowC.IsDeleted
                    orderby rowC.CreationDateTime descending
                    select new LogbookDto()
                    {
                        Id = rowC.Id,
                        ProgramId = rowC.ProgramId,
                        DateOfActivity = rowC.DateOfActivity,
                        UserId = rowC.UserId,
                        WeekStartDate = rowC.WeekStartDate,
                        WeekEndDate = rowC.WeekEndDate,
                        TimeHours = rowC.TimeHours,
                        TimeMinutes = rowC.TimeMinutes,
                        WeekNumber = rowC.WeekNumber,
                        ActivityDescription = rowC.ActivityDescription,
                        ActivityType = rowC.ActivityType,
                        IsSubmited = rowC.IsSubmited,
                        CreationDateTime = rowC.CreationDateTime
                    };
            if (loggedInUserId != 0)
            {
                q = from row in q
                    where row.UserId == loggedInUserId
                    select row;
            }

            var fcount = q;
            return fcount.ToList();

        }


        public DBResult CreateOrUpdateLogBook(LogbookInput logtime)
        {
            bool hasError = false;
            string errorText = "";

            if (logtime.Id == 0)
            {
                try
                {

                    var newTimeLog = new db_LogBook()
                    {
                        UserId = logtime.UserId,
                        ProgramId = logtime.ProgramId,
                        ActivityType = logtime.ActivityType,
                        WeekStartDate = logtime.DateOfActivity.AddDays(-(int)logtime.DateOfActivity.DayOfWeek + (int)DayOfWeek.Monday),
                        //WeekStartDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday),
                        //first day of the week
                        WeekEndDate = logtime.DateOfActivity.AddDays(-(int)logtime.DateOfActivity.DayOfWeek + (int)DayOfWeek.Friday),
                        //WeekEndDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Friday),
                        //last day of the week
                        TimeHours = logtime.TimeHours,
                        TimeMinutes = logtime.TimeMinutes,
                        DateOfActivity = logtime.DateOfActivity,
                        ActivityDescription = logtime.ActivityDescription,
                        IsSubmited = false,
                        IsDeleted= false,
                        CreationDateTime = DateTime.Now,
                    };
                    var results = newTimeLog._insert();
                    if (!results.Result.Success)
                    {
                        hasError = true;
                        errorText = results.Result.DescripText;
                    }
                    if (!hasError)
                    {
                        DataAccess.metadata.SaveChanges();
                    } //save    
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                db_LogBook oldLogbook = DataAccess.metadata.db_LogBook.Find(logtime.Id);
                if (oldLogbook == null)
                {
                    hasError = true;
                    return new DBResult
                    {
                        Status = !hasError ? "Success" : "Fail",
                        DescripText = "Record not found!",
                        Success = false
                    };
                }
                else
                {
                   
                    oldLogbook.ActivityType = logtime.ActivityType;
                    oldLogbook.DateOfActivity = logtime.DateOfActivity;
                    oldLogbook.WeekStartDate =
                        logtime.DateOfActivity.AddDays(-(int) logtime.DateOfActivity.DayOfWeek + (int) DayOfWeek.Monday);
                    oldLogbook.WeekEndDate =
                        logtime.DateOfActivity.AddDays(-(int) logtime.DateOfActivity.DayOfWeek + (int) DayOfWeek.Friday);
                    oldLogbook.ActivityDescription = logtime.ActivityDescription;
                    oldLogbook.TimeHours = logtime.TimeHours;
                    oldLogbook.TimeMinutes = logtime.TimeMinutes;
                    oldLogbook.ModifiedByUser = logtime.UserId;
                    oldLogbook.LastdateModified=DateTime.Now;
                    

                    oldLogbook._update();

                    DataAccess.SaveChanges();

                }
            }


            return new DBResult { Status = !hasError ? "Success" : "Fail", DescripText = errorText };
        }
        public async Task<DBResult> ActiveStatus(long id)
        {
            bool hasError = false;
            string errorText = "";
            try
            {
                db_LogBook logBook = DataAccess.metadata.db_LogBook.Find(id);
                if (logBook == null)
                {
                    hasError = true;
                    return new DBResult { Status = !hasError ? "Success" : "Fail", DescripText = "Record not found!", Success = false };
                }
                else
                {
                    logBook.IsSubmited = logBook.IsSubmited ? false : true;
                    var results = logBook._update();
                    if (!results.Result.Success)
                    {
                        hasError = true;
                        errorText = results.Result.DescripText;
                    }

                    if (!hasError)
                    {
                        DataAccess.metadata.SaveChanges();

                    }//save 
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return new DBResult { Status = !hasError ? "Success" : "Fail", DescripText = errorText };

        }

        public async Task<DBResult> DeleteStatus(long id)
        {
            bool hasError = false;
            string errorText = "";
            try
            {
                db_LogBook logBook = DataAccess.metadata.db_LogBook.Find(id);
                if (logBook == null)
                {
                    hasError = true;
                    return new DBResult { Status = !hasError ? "Success" : "Fail", DescripText = "Record not found!", Success = false };
                }
                else
                {
                    logBook.IsDeleted = logBook.IsDeleted ? false : true;
                    

                    var results = logBook._update();
                    if (!results.Result.Success)
                    {
                        hasError = true;
                        errorText = results.Result.DescripText;
                    }

                    if (!hasError)
                    {
                        DataAccess.metadata.SaveChanges();

                    }//save 
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return new DBResult { Status = !hasError ? "Success" : "Fail", DescripText = errorText };

        }
    }
}
