using System;
using System.Linq;
using System.Threading.Tasks;
using sgrc.DikizaCS.DAL.User.Dto;
using sgrc.DikizaCS.DAL.Utils;
using sgrc.Encrypt;
using EntityFramework.Extensions;
using sgrc.DikizaCS.DAL.Client.Dto;
using sgrc.DikizaCS.DAL.Program.Dto;

namespace sgrc.DikizaCS.DAL.User
{
    public class UserAppService : IUserAppService
    {
        public  async Task<DBResult> CreateOrUpdateUser(RegisterInput input)
        {

            bool hasError = false;
            string errorText = String.Empty;
            DBResult results;
            try
            {
                switch (input.Id)
                {
                    case 0:
                        var newUser = new db_User()
                        {
                            ClientId = input.ClientId,
                            Name = input.Name,
                            Surname = input.Surname,
                            Email = input.Email,
                            Password = PasswordManager.Encrypt(input.Password),
                            PhoneNumber = input.PhoneNumber,
                            TelephoneNumber = input.TelephoneNumber,
                            SecurityStamp = Guid.NewGuid(),
                            IsActive = true,
                            IsAdmin = false,
                            IsDeleted = false,
                            IsEmailConfirmed = false,
                            CreationDateTime = DateTime.Now,
                        };
                        results = await newUser._insert();
                        if (!results.Success)
                        {
                            hasError = true;
                            errorText = results.DescripText;
                        }
                        break;
                    default:
                        var oldUser = DataAccess.metadata.db_User.Find(input.Id);
                        if (oldUser == null)
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
                            oldUser.Name = input.Name;
                            oldUser.Surname = input.Surname;
                            oldUser.PhoneNumber = input.PhoneNumber;
                            oldUser.TelephoneNumber = input.TelephoneNumber;
                            oldUser.Email = input.Email;
                            oldUser.LastModifiedDate = DateTime.Now;
                        }
                        results = await oldUser._update();
                        if (!results.Success)
                        {
                            hasError = true;
                            errorText = results.DescripText;
                        }
                        break;
                }
                if (!hasError){
                    DataAccess.SaveChanges();
                }
            }
            catch (Exception e){
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

        public  async Task<UserAuthOutPut> Login(LoginInput input)
        {
           
            var result = new UserAuthOutPut();
            var q = (from row in DataAccess.metadata.db_User
                         where row.Email == input.Username && row.IsActive && !row.IsDeleted
                         select new
                         {
                             Id = row.Id,
                             ClientId=row.ClientId,
                             ProgramId=row.ProgramId,
                              Name = row.Name,
                             Surname = row.Surname,
                             Email = row.Email,
                             Password = row.Password,
                             PhoneNumber = row.PhoneNumber,
                             TelephoneNumber = row.TelephoneNumber,
                             SecurityStamp = row.SecurityStamp,
                             IsActive = row.IsActive,
                             IsAdmin = row.IsAdmin,
                             IsDeleted = row.IsDeleted,
                             IsEmailConfirmed = row.IsEmailConfirmed,
                             CreationDateTime = row.CreationDateTime,
                         });
            var qClientUser = from rowC in DataAccess.metadata.db_Client
                             where rowC.Email == input.Username
                             && rowC.IsActive
                             select new
                             {

                                 Id = rowC.Id,
                                 Name = rowC.Name,
                                 Password=rowC.Password,
                                 ContactName = rowC.ContactName,
                                 ContactSurname = rowC.ContactSurname,
                                 ContactEmail = rowC.ContactEmail,
                                 ContactPhone = rowC.ContactPhone,
                                 PhoneNumber = rowC.ContactPhone,
                                 Email = rowC.Email,
                                
                                 IsActive = rowC.IsActive

                             };

            var qF = q.FutureFirstOrDefault();
            var userV = qF.Value;

            var qClientUserF = qClientUser.FutureFirstOrDefault();
            var qClientUserV = qClientUserF.Value;

            if (userV != null)
            {
                result.User = new UserAuthOutPut
                {
                   
                    Id = userV.Id,
                    ClientId = userV.ClientId ?? 0,
                    ProgramId = userV.ProgramId ?? 0,
                    Name = userV.Name,
                    Surname = userV.Surname,
                    Email = userV.Email,
                    Password = userV.Password,
                    PhoneNumber = userV.PhoneNumber,
                    TelephoneNumber = userV.TelephoneNumber,
                    SecurityStamp = userV.SecurityStamp,
                    IsActive = userV.IsActive,
                    IsAdmin = userV.IsAdmin,
                    IsDeleted = userV.IsDeleted,
                    IsEmailConfirmed = userV.IsEmailConfirmed,
                    CreationDateTime = userV.CreationDateTime,
                    IsAuthenticated = PasswordManager.Verify(input.Password, userV.Password),

                };

              
            }
            else if (qClientUserV!=null)
            {
                result.ClientUser = new ClientDto()
                {
                    Id = qClientUserV.Id,
                    Name = qClientUserV.Name,
                    IsAuthenticated = PasswordManager.Verify(input.Password, qClientUserV.Password),
                    ContactName = qClientUserV.ContactName,
                    ContactSurname = qClientUserV.ContactSurname,
                    ContactEmail = qClientUserV.ContactEmail,
                    ContactPhone = qClientUserV.ContactPhone,
                    PhoneNumber = qClientUserV.ContactPhone,
                    Email = qClientUserV.Email,

                    IsActive = qClientUserV.IsActive
                };
            }
            return result;
        }

        public async Task<DBResult> ActiveStatus(long id)
        {
            bool hasError = false;
            string errorText = "";
            try
            {
                var user = DataAccess.metadata.db_User.Find(id);
                if (user == null)
                {
                    hasError = true;
                    return new DBResult { Status = !hasError ? "Success" : "Fail", DescripText = "Record not found!", Success = false };
                }
                else
                {
                    user.IsActive = !user.IsActive;
                    var results = await user._update();
                    if (!results.Success)
                    {
                        hasError = true;
                        errorText = results.DescripText;
                    }

                    if (!hasError)
                    {
                        DataAccess.metadata.SaveChanges();

                    }
                }

            }
            catch (Exception e)
            {
                hasError = true;
                return new DBResult { Status = !hasError ? "Success" : "Fail", DescripText = e.Message };
            }
            return new DBResult { Status = !hasError ? "Success" : "Fail", DescripText = errorText };

        }


        public UserDto GetByClient(long clientUserId)
        {
            var qStudents = from rowC in DataAccess.metadata.db_User
                            where rowC.ClientId== clientUserId &&  !rowC.IsDeleted &  !rowC.IsAdmin
                            select new UserDto
                           {
                               Id = rowC.Id,
                               Name = rowC.Name,
                              Surname = rowC.Surname,
                               Email = rowC.Email,
                              CreationDateTime = rowC.CreationDateTime,
                               IsActive = rowC.IsActive
                           };
            var fStudents = qStudents.OrderByDescending(d => d.CreationDateTime).ToList();

            var qProgramss = from rowC in DataAccess.metadata.db_Program
                            where rowC.ClientId == clientUserId && !rowC.IsDeleted & rowC.IsActive
                            select new ProgramDto()
                            {
                                Id = rowC.Id,
                                Name = rowC.Name,
                                IsActive = rowC.IsActive
                            };
            var fPrograms = qProgramss.OrderByDescending(p => p.Name).ToList();

            return new UserDto
            {
                UserList = fStudents,
                ProgramList = fPrograms
            };

        }

    }
}
