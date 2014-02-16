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
    
    public partial class Contact
    {
        public Contact()
        {
            this.AppUserContacts = new HashSet<AppUserContact>();
            this.ContactDetails = new HashSet<ContactDetail>();
            this.ContactEmailAddresses = new HashSet<ContactEmailAddress>();
            this.ContactLinks = new HashSet<ContactLink>();
            this.ContactLinks1 = new HashSet<ContactLink>();
            this.ContactPhoneNumbers = new HashSet<ContactPhoneNumber>();
        }
    
        public System.Guid ContactId { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string NickName { get; set; }
        public Nullable<bool> IsSelf { get; set; }
        public Nullable<bool> IsPrimary { get; set; }
    
        public virtual ICollection<AppUserContact> AppUserContacts { get; set; }
        public virtual ICollection<ContactDetail> ContactDetails { get; set; }
        public virtual ICollection<ContactEmailAddress> ContactEmailAddresses { get; set; }
        public virtual ICollection<ContactLink> ContactLinks { get; set; }
        public virtual ICollection<ContactLink> ContactLinks1 { get; set; }
        public virtual ICollection<ContactPhoneNumber> ContactPhoneNumbers { get; set; }
    }
}
