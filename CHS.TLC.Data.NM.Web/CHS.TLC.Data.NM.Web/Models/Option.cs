//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CHS.TLC.Data.NM.Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Option
    {
        public Option()
        {
            this.RoleOption = new HashSet<RoleOption>();
        }
    
        public int OptionId { get; set; }
        public Nullable<int> FatherId { get; set; }
        public string Section { get; set; }
        public string SubSection { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool IsVisible { get; set; }
        public string State { get; set; }
    
        public virtual ICollection<RoleOption> RoleOption { get; set; }
    }
}