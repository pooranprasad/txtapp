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
    
    public partial class AppUser
    {
        public AppUser()
        {
            this.AppUserContacts = new HashSet<AppUserContact>();
            this.AppUserSources = new HashSet<AppUserSource>();
            this.Devices = new HashSet<Device>();
            this.Tokens = new HashSet<Token>();
        }
    
        public System.Guid AppUserId { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string NickName { get; set; }
        public string Token { get; set; }
    
        public virtual ICollection<AppUserContact> AppUserContacts { get; set; }
        public virtual ICollection<AppUserSource> AppUserSources { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
    }
}
