using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sgrc.DikizaCS.Areas.User.Models
{
    public class ProgramModel
    {
        
        public long Id { get; set; }
        public long ClientId { get; set; }
        [Required(ErrorMessage = "Program Name Field Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Program Description Field Required")]
        public string Description { get; set; }

        public bool IsActive { get; set; }

    }
}