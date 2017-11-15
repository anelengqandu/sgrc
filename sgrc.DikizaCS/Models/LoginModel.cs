using System.ComponentModel.DataAnnotations;

namespace sgrc.DikizaCS.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}