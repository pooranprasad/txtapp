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
    
    public partial class ContactPhoneNumber
    {
        public System.Guid ContactPhoneNumberId { get; set; }
        public System.Guid ContactId { get; set; }
        public System.Guid PhoneNumberId { get; set; }
        public string Extn { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsPersonal { get; set; }
        public bool IsEnterprise { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Contact Contact { get; set; }
        public virtual PhoneNumber PhoneNumber { get; set; }
    }
}
