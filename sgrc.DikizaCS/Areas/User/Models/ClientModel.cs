﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sgrc.DikizaCS.Areas.User.Models
{
    public class ClientModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string ContactSurname { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string ContactPhone { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string ContactEmail { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDateTime { get; set; }

    }
}