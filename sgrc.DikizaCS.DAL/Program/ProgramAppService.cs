using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using sgrc.DikizaCS.DAL.Program.Dto;
using sgrc.DikizaCS.DAL.Utils;

namespace sgrc.DikizaCS.DAL.Program
{
    public class ProgramAppService : IProgramAppService
    {
        public async Task<DBResult> CreateOrUpdateProgram(CreateOrUpdateInput input)
        {
            bool hasError = false;
            string errorText = String.Empty;
            DBResult results;
            try
            {
                switch (input.Id)
                {
                    case 0:
                        var newProgram = new db_Program()
                        {
                            Name = input.Name,
                            Description = input.Description,
                            IsActive = true,
                            CreationDateTime = DateTime.Now,
                            IsDeleted = false,
                            CreatedByUser = input.CreatedByUser,

                        };
                        results = await newProgram._insert();
                        if (!results.Success)
                        {
                            hasError = true;
                            errorText = results.DescripText;
                        }
                        break;
                    default:
                        var oldProgram = DataAccess.metadata.db_Program.Find(input.Id);
                        if (oldProgram == null)
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
                            oldProgram.Name = input.Name;
                            oldProgram.Description = input.Description;
                            oldProgram.ModifiedDate = DateTime.Now;
                            oldProgram.ModifiedByUser = input.ModifiedByUser;
                            oldProgram.IsActive = input.IsActive;
                        }
                        results = await oldProgram._update();
                        if (!results.Success)
                        {
                            hasError = true;
                            errorText = results.DescripText;
                        }
                        break;
                }
                if (!hasError)
                {
                    DataAccess.SaveChanges();
                }
            }
            catch (Exception e)
            {
                hasError = true;
                return new DBResult
                {
                    Status = !hasError ? "Success" : "Fail",
                    DescripText = e.Message,
                    Success = false
                };
            }
            return new DBResult { Status = !hasError ? "Success" : "Fail", DescripText = errorText };
        }

        public List<ProgramDto> GetPrograms(ref PagingInfo paging)
        {
            var q = from rowC in DataAccess.metadata.db_Program

                    orderby rowC.Name descending

                    select new ProgramDto
                    {
                        Id = rowC.Id,
                        Name = rowC.Name,
                        Description = rowC.Description,
                        CreationDateTime = rowC.CreationDateTime,
                        IsActive = rowC.IsActive,

                    };

            // do search filter
            if (!string.IsNullOrEmpty(paging.SearchString))
            {
                var searchString = paging.SearchString;
                q = from row in q
                    where row.Name.Contains(searchString) || row.Description.Contains(searchString)
                    select row;
            }



            //futures
            var fq = q.Future().Skip(paging.Skip).Take(paging.Take);
            var fcount = q.FutureCount();


            return fq.ToList();

        }

        public List<ProgramDto> GetClientPrograms(ref PagingInfo paging,long? clientId)
        {
            var q = from rowC in DataAccess.metadata.db_Program
                    where rowC.ClientId==clientId
                    orderby rowC.Name descending

                    select new ProgramDto
                    {
                        Id = rowC.Id,
                        Name = rowC.Name,
                        Description = rowC.Description,
                        CreationDateTime = rowC.CreationDateTime,
                        IsActive = rowC.IsActive,

                    };

            // do search filter
            if (!string.IsNullOrEmpty(paging.SearchString))
            {
                var searchString = paging.SearchString;
                q = from row in q
                    where row.Name.Contains(searchString) || row.Description.Contains(searchString)
                    select row;
            }



            //futures
            var fq = q.Future().Skip(paging.Skip).Take(paging.Take);
            var fcount = q.FutureCount();


            return fq.ToList();

        }

        public int ProgramsCounter()
        {
            var q = from rowC in DataAccess.metadata.db_Program
                select rowC;
            return q.Count();

        }
        public async Task<DBResult> ActiveStatus(long id)
        {
            bool hasError = false;
            string errorText = "";
            try
            {
                db_Program program = DataAccess.metadata.db_Program.Find(id);
                if (program == null)
                {
                    hasError = true;
                    return new DBResult { Status = !hasError ? "Success" : "Fail", DescripText = "Record not found!", Success = false };
                }
                else
                {
                    program.IsActive = program.IsActive ? false : true;
                    var results = program._update();
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


        public ProgramDto GetAsync(long id)
        {
            var results =  DataAccess.metadata.db_Program.FindAsync(id);
            if (results == null) return new ProgramDto();
            var data = new ProgramDto()
            {
                Id = results.Id,
                Name = results.Result.Name,
                Description = results.Result.Description,
                IsActive = results.Result.IsActive
            };
            return data;
        }
    }

}
