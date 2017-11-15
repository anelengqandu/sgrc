using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sgrc.DikizaCS.DAL.Client.Dto
{
   public class ClientDto
    {
        public List<ClientDto> ClientList { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactName { get; set; }
        public string SecurityStampId { get; set; }
        public string ContactSurname { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public DateTime CreationDateTime { get; set; }

       public string CreationDateTimeS
       {
            get
            {
                return CreationDateTime.ToString("dd MMM yyyy");
            }
        }
        public bool IsAuthenticated { get; set; }

        public bool IsActive { get; set; }
    }
}
