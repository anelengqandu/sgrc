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
    
    public partial class db_Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public db_Role()
        {
            this.db_UserRole = new HashSet<db_UserRole>();
        }
    
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<db_UserRole> db_UserRole { get; set; }
    }
}