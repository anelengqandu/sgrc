//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sgrc.DikizaCS.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class db_UserRole
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long UserId { get; set; }
    
        public virtual db_Role db_Role { get; set; }
        public virtual db_User db_User { get; set; }
    }
}
