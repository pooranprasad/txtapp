//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace kontxt.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AppUserContact
    {
        public System.Guid AppUserContactId { get; set; }
        public System.Guid AppUserId { get; set; }
        public Nullable<System.Guid> AppUserSourceId { get; set; }
        public System.Guid ContactId { get; set; }
        public bool IsSelf { get; set; }
    
        public virtual AppUser AppUser { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
