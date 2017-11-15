using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sgrc.DikizaCS.Areas.User.Models
{
    public class StudentModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname Required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Email Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TelephoneNumber { get; set; }
       
    }
}