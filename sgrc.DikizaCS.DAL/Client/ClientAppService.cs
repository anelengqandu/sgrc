using System;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using sgrc.DikizaCS.DAL.Client.Dto;
using sgrc.DikizaCS.DAL.Program;
using sgrc.DikizaCS.DAL.User.Dto;
using sgrc.DikizaCS.DAL.Utils;
using sgrc.Encrypt;

namespace sgrc.DikizaCS.DAL.Client
{
    public class ClientAppService: IClientAppService
    {
        private readonly IProgramAppService _programRepository;
        public ClientAppService()
        {
            _programRepository = new ProgramAppService();
        }
        public async Task<DBResult> CreateOrUpdateClient(CreateOrUpdateInput input)
        {
            bool hasError = false;
            string errorText = String.Empty;
            DBResult results;
            try
            {
                switch (input.Id)
                {
                    case 0:
                        var newClient = new db_Client()
                        {
                            Name = input.Name,
                            PhoneNumber = input.PhoneNumber,
                            Email = input.Email,
                            Password = PasswordManager.Encrypt(input.Password),
                            //SecurityStampId = Guid.NewGuid().ToString("N").Replace("-", "").Substring(0, 18),
                            SecurityStampId = input.SecurityStampId,
                            ContactName = input.ContactName,
                            ContactSurname = input.ContactSurname,
                            ContactPhone = input.ContactPhone,
                            ContactEmail = input.ContactEmail,
                            IsActive = true,
                            CreationDate = DateTime.Now

                        };
                        results = await newClient._insert();
                        if (!results.Success)
                        {
                            hasError = true;
                            errorText = results.DescripText;
                        }
                        break;
                    default:
                        var oldClient = DataAccess.metadata.db_Client.Find(input.Id);
                        if (oldClient == null)
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
                            oldClient.Name = input.Name;
                            oldClient.PhoneNumber = input.PhoneNumber;
                            oldClient.Email = input.Email;
                            oldClient.ContactName = input.ContactName;
                            oldClient.ContactSurname = input.ContactSurname;
                            oldClient.ContactPhone = input.ContactPhone;
                            oldClient.ContactEmail = input.ContactEmail;
                            oldClient.IsActive = input.IsActive;

                        }
                        results = await oldClient._update();
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
        public ClientDto GetAll()
        {
            var qClients = from rowC in DataAccess.metadata.db_Client
                           select new ClientDto
                           {
                               Id = rowC.Id,
                               Name = rowC.Name,
                               ContactName = rowC.ContactName,
                               ContactSurname = rowC.ContactSurname,
                               ContactEmail = rowC.ContactEmail,
                               ContactPhone = rowC.ContactPhone,
                               PhoneNumber = rowC.ContactPhone,
                               Email = rowC.Email,
                               CreationDateTime = rowC.CreationDate,
                               IsActive = rowC.IsActive
                           };
            var fClients = qClients.OrderByDescending(d=>d.CreationDateTime).ToList();
           
            return new ClientDto
            {
                ClientList = fClients,
            };

        }
        public async Task<DBResult> AccountConfirmation(string securitystamp)
        {
            bool hasError = false;
            DBResult results;
            string errorText = String.Empty;
            var client = db_Client.GetBySecurityStampId(securitystamp);
            if (client == null)
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
                client.SecurityStampId = securitystamp;
                client.IsAccountConfirmed = true;
                client.IsActive = true;
            }
            results = await client._update();
            if (!results.Success)
            {
                hasError = true;
                errorText = results.DescripText;
            }

            if (!hasError)
            {
                DataAccess.SaveChanges();
            }
            return new DBResult { Status = !hasError ? "Success" : "Fail", DescripText = errorText };
        }
        public ClientOutput GetClientData(ref PagingInfo paging, long? clientId)
        {

            var qClients = from rowC in DataAccess.metadata.db_Client
                           where rowC.Id == clientId
                           select new ClientDto
                           {
                               Id = rowC.Id,
                               Name = rowC.Name,
                               ContactName = rowC.ContactName,
                               ContactSurname = rowC.ContactSurname,
                               ContactEmail = rowC.ContactEmail,
                               ContactPhone = rowC.ContactPhone,
                               PhoneNumber = rowC.ContactPhone,
                               Email = rowC.Email,
                               CreationDateTime = rowC.CreationDate,
                               IsActive = rowC.IsActive
                           };

            var qClientStudents = from row in DataAccess.metadata.db_User
                join rowClient in DataAccess.metadata.db_Client 
                on row.ClientId equals rowClient.Id
                where row.ClientId == clientId && !row.IsAdmin
                select new UserDto()
                {
                    Id = row.Id,
                    Name = row.Name,
                    Surname = row.Surname,
                    Email = row.Email,
                    PhoneNumber = row.PhoneNumber,
                    TelephoneNumber = row.TelephoneNumber,
                    SecurityStamp = row.SecurityStamp,
                    IsActive = row.IsActive,
                    IsAdmin = row.IsAdmin,
                    IsDeleted = row.IsDeleted,
                    CreationDateTime = row.CreationDateTime,
                };
            // do search filter
            if (!string.IsNullOrEmpty(paging.SearchString))
            {
                var searchString = paging.SearchString;
                qClientStudents = from row in qClientStudents
                                  where row.Name.Contains(searchString)
                                  || row.Surname.Contains(searchString)
                                  || row.Email.Contains(searchString)
                                  || row.PhoneNumber.Contains(searchString)
                                  || row.TelephoneNumber.Contains(searchString)

                    select row;
            }

            var fq = qClientStudents.Future().Skip(paging.Skip).Take(paging.Take);
            var fcount = qClientStudents.FutureCount();
            paging.ResultCount = fcount.Value;
            var fClients = qClients.FirstOrDefault();
            var fClientPrograms = _programRepository.GetClientPrograms(ref paging, clientId);

            var result = new ClientOutput();
            return result= new ClientOutput
            {
                 Students = fq.ToList(),
                Client = fClients,
                ClientPrograms = fClientPrograms
            };


        }
        public int ClientCounter()
        {
            var q = from rowC in DataAccess.metadata.db_Client
                    select rowC;
            return q.Count();

        }
    }
}
