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
    
    public partial class SourceType
    {
        public SourceType()
        {
            this.Sources = new HashSet<Source>();
        }
    
        public int SourceTypeId { get; set; }
        public Nullable<int> ParentSourceTypeId { get; set; }
        public string SourceTypeName { get; set; }
    
        public virtual ICollection<Source> Sources { get; set; }
    }
}
