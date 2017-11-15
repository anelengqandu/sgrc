using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sgrc.DikizaCS.DAL.Program.Dto;
using sgrc.DikizaCS.DAL.User.Dto;

namespace sgrc.DikizaCS.DAL.Client.Dto
{
   public class ClientOutput
    {
       public ClientDto Client { get; set; }
       public List<UserDto> Students { get; set; }
        public List<ProgramDto> ClientPrograms { get; set; }
    }
}
