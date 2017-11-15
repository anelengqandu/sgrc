using System.ComponentModel.DataAnnotations;

namespace sgrc.DikizaCS.Models
{
    public class RegisterModel
    {
        public long ClientId { get; set; }
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname Required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Email Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
       
        public string Password { get; set; }

        public string PhoneNumber { get; set; }
        public string TelephoneNumber { get; set; }
    }
}