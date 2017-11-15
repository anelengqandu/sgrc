using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sgrc.DikizaCS.DAL.Program.Dto
{
   public class CreateOrUpdateInput
    {
        public long Id { get; set; }
        public long? ClientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedByUser { get; set; }
        public long ModifiedByUser { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string CreationDateTimeS=> CreationDateTime.ToString("dd MM yyyy");
        public DateTime ModifiedDate { get; set; } 
        public string ModifiedDateS => ModifiedDate.ToString("dd MM yyyy");

    }
}
