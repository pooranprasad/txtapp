//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace kontxt.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Source
    {
        public Source()
        {
            this.AppUserSources = new HashSet<AppUserSource>();
        }
    
        public int SourceId { get; set; }
        public int SourceTypeId { get; set; }
        public string SourceName { get; set; }
    
        public virtual ICollection<AppUserSource> AppUserSources { get; set; }
        public virtual SourceType SourceType { get; set; }
    }
}
