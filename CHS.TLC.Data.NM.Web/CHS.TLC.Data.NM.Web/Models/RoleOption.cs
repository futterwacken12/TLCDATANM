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
    
    public partial class RoleOption
    {
        public int RoleOptionId { get; set; }
        public int OptionId { get; set; }
        public int RoleId { get; set; }
        public string State { get; set; }
    
        public virtual Option Option { get; set; }
        public virtual Role Role { get; set; }
    }
}
