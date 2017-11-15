using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sgrc.DikizaCS.Areas.User.Models
{
    public class LogbookModel
    {
        
        public long Id { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public DateTime DateOfActivity { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public string ActivityType { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public string ActivityDescription { get; set; }

        public decimal TimeHours { get; set; }
       
        public decimal TimeMinutes { get; set; }
    }
}