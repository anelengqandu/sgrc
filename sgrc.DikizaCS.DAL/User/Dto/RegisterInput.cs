using System;

namespace sgrc.DikizaCS.DAL.User.Dto
{
   public class RegisterInput
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public Guid SecurityStamp { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fullname => Name + " " + Surname;
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailConfirmationCode { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTime LastLoginDateTime { get; set; }
        public string LastLoginDateTimeS => LastLoginDateTime.ToString("dd MMM yyyy");
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedDateS => LastModifiedDate.ToString("dd MMM yyyy");
        public bool IsDeleted { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string CreationDateTimeS => CreationDateTime.ToString("dd MMM yyyy");

    }
}
